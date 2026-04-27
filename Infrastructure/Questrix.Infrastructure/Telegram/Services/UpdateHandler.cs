using MediatR;
using Questrix.Application.Features.Sessions.Commands.Start;
using Questrix.Application.Features.Sessions.Commands.SubmitResponse;
using Questrix.Application.Features.Sessions.Queries.GetCurrentQuestion;
using Questrix.Infrastructure.Telegram.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Questrix.Infrastructure.Telegram.Services
{
    public class UpdateHandler(IMediator mediator, ITelegramKeyboardService telegramKeyboardService) : IUpdateHandler
    {
        private readonly IMediator mediator = mediator;
        private readonly ITelegramKeyboardService telegramKeyboardService = telegramKeyboardService;

        public async Task HandleAsync(ITelegramBotClient telegramBotClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                string? text = update.Message?.Text;
                if (text is null)
                    return;

                string? userId = update.Message?.From?.Id.ToString();
                if (userId is null)
                    return;

                // 1. Check active session
                GetCurrentQuestionSessionQueryResponse? currentQuestion = await mediator.Send(new GetCurrentQuestionSessionQueryRequest
                {
                    UserId = userId,
                }, cancellationToken);

                if (currentQuestion is null)
                {
                    // No session → treat as invitation code
                    await telegramBotClient.SendMessage(chatId: update.Message!.Chat.Id, text: (await mediator.Send(new StartSessionCommandRequest
                    {
                        InvitationCode = text,
                        UserId = userId
                    }, cancellationToken)).FirstQuestion, cancellationToken: cancellationToken);

                    return;
                }

                // Active session → submit answer
                SubmitResponseSessionCommandResponse response = await mediator.Send(new SubmitResponseSessionCommandRequest
                {
                    UserId = userId,
                    Answer = text
                }, cancellationToken);

                if (response.IsCompleted)
                {
                    await telegramBotClient.SendMessage(update.Message!.Chat.Id, "Survey completed. Thank you!", cancellationToken: cancellationToken);

                    return;
                }

                await telegramBotClient.SendMessage(update.Message!.Chat!.Id, response.NextQuestion, replyMarkup: telegramKeyboardService.Build(currentQuestion.Options, currentQuestion.Type), cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                await telegramBotClient.SendMessage(update.Message!.Chat!.Id, $"Something went wrong. {e}", cancellationToken: cancellationToken);
            }
        }
    }
}
