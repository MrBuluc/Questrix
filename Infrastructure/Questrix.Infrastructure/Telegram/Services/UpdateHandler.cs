using MediatR;
using Questrix.Application.DTOs;
using Questrix.Application.Features.Sessions.Commands.Start;
using Questrix.Application.Features.Sessions.Commands.SubmitResponse;
using Questrix.Application.Features.Sessions.Queries.GetCurrentQuestion;
using Questrix.Application.Features.Surveys.Queries.GetDefinition;
using Questrix.Infrastructure.Telegram.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Questrix.Infrastructure.Telegram.Services
{
    public class UpdateHandler(IMediator mediator, ITelegramKeyboardService telegramKeyboardService, ITelegramTextFormatter telegramTextFormatter) : IUpdateHandler
    {
        private readonly IMediator mediator = mediator;
        private readonly ITelegramKeyboardService telegramKeyboardService = telegramKeyboardService;
        private readonly ITelegramTextFormatter telegramTextFormatter = telegramTextFormatter;

        public async Task HandleAsync(ITelegramBotClient telegramBotClient, Update update, CancellationToken cancellationToken)
        {
            string? text = update.Message?.Text;
            if (text is null)
                return;

            string? userId = update.Message?.From?.Id.ToString();
            if (userId is null)
                return;

            long? chatId = update.Message?.Chat.Id;
            if (chatId is null)
                return;

            try
            {
                // 1. Check active session
                GetCurrentQuestionSessionQueryResponse? currentQuestion = await mediator.Send(new GetCurrentQuestionSessionQueryRequest
                {
                    UserId = userId,
                }, cancellationToken);

                if (currentQuestion is null)
                {
                    // No session → treat as invitation code
                    GetSurveyDefinitionQueryResponse getSurveyDefinitionQueryResponse = await mediator.Send(new GetSurveyDefinitionQueryRequest
                    {
                        InvitationCode = text
                    }, cancellationToken);
                    await telegramBotClient.SendMessage(update.Message!.Chat.Id, getSurveyDefinitionQueryResponse.Title, cancellationToken: cancellationToken);
                    await telegramBotClient.SendMessage(update.Message!.Chat.Id, getSurveyDefinitionQueryResponse.Description, cancellationToken: cancellationToken);

                    await SendNode(telegramBotClient, chatId.Value, (await mediator.Send(new StartSessionCommandRequest
                    {
                        UserId = userId,
                        InvitationCode = text
                    }, cancellationToken)).SurveyNode, cancellationToken);

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
                    await telegramBotClient.SendMessage(chatId, "Survey completed. Thank you!",  replyMarkup: new ReplyKeyboardRemove(), cancellationToken: cancellationToken);

                    return;
                }

                await SendNode(telegramBotClient, chatId.Value, response.SurveyNode, cancellationToken);
            }
            catch (Exception e)
            {
                await telegramBotClient.SendMessage(update.Message!.Chat!.Id, $"Something went wrong. {e}", cancellationToken: cancellationToken);
            }
        }

        private async Task SendNode(ITelegramBotClient telegramBotClient, long chatId, SurveyNodeDTO surveyNodeDTO, CancellationToken cancellationToken) => await telegramBotClient.SendMessage(chatId, telegramTextFormatter.Format(surveyNodeDTO), replyMarkup: telegramKeyboardService.Build(surveyNodeDTO.Type, surveyNodeDTO.Options, surveyNodeDTO.Metadata), cancellationToken: cancellationToken);
    }
}
