using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Questrix.Application.DTOs;
using Questrix.Application.Exceptions;
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
    public class UpdateHandler(ITelegramKeyboardService telegramKeyboardService, ITelegramTextFormatter telegramTextFormatter, IServiceScopeFactory serviceScopeFactory) : IUpdateHandler
    {
        private readonly ITelegramKeyboardService telegramKeyboardService = telegramKeyboardService;
        private readonly ITelegramTextFormatter telegramTextFormatter = telegramTextFormatter;
        private readonly IServiceScopeFactory serviceScopeFactory = serviceScopeFactory;

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
                //0. Greetings
                if (text == "/start")
                {
                    await telegramBotClient.SendMessage(chatId, "Merhaba 👋\r\nBen QuestrixBot, akıllı anket asistanınız.\r\n\r\nAnketleri sohbet tarzında yürüteceğim; sıkıcı formlar yok, sadece basit bir sohbet.\r\n\r\n📌 Başlamak için:\r\nDavet kodunuzu gönderin.\r\n\r\nBundan sonra, adım adım sorular sorup yanıtlarınızı toplayacağım.\r\n\r\nBaşlayalım 🚀", cancellationToken: cancellationToken);

                    return;
                }

                await using AsyncServiceScope serviceScope = serviceScopeFactory.CreateAsyncScope();
                IMediator mediator = serviceScope.ServiceProvider.GetRequiredService<IMediator>();

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
                    await telegramBotClient.SendMessage(chatId, "🎉 İşte bu kadar—işiniz bitti!\r\n\r\nYanıtlarınız kaydedildi.\r\nBaşka bir davet kodunuz varsa, onu göndererek yeni bir ankete başlayabilirsiniz.",  replyMarkup: new ReplyKeyboardRemove(), cancellationToken: cancellationToken);

                    return;
                }

                await SendNode(telegramBotClient, chatId.Value, response.SurveyNode, cancellationToken);
            }
            catch (InvitationCodeNotFoundException)
            {
                await telegramBotClient.SendMessage(update.Message!.Chat.Id, "❌ Bu kodla ilgili bir anket bulamadım.\r\n\r\nLütfen kodu doğru girdiğinizden emin olun ve tekrar deneyin.", cancellationToken: cancellationToken);

                throw;
            }
            catch (InvitationCodeMaxUsegeException)
            {
                await telegramBotClient.SendMessage(update.Message!.Chat.Id, "⚠️ Bu davet kodunun kullanım limiti dolmuştur.\r\n\r\nAnket, artık yeni katılımcı kabul etmemektedir.", cancellationToken: cancellationToken);
            }
            catch (InvitationCodeExpiredException)
            {
                await telegramBotClient.SendMessage(update.Message!.Chat.Id, "⚠️ Davet kodu geçersiz.\r\n\r\nGirdiğiniz kod bulunamadı veya süresi dolmuş olabilir.\r\nLütfen kodu kontrol edip tekrar deneyin.", cancellationToken: cancellationToken);
            }
            catch (MessageNotValidateException)
            {
                await telegramBotClient.SendMessage(update.Message!.Chat.Id, "“❌ Lütfen aşağıdaki seçeneklerden birini seçin.”", cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                await telegramBotClient.SendMessage(update.Message!.Chat.Id, $"Something went wrong. {e}", cancellationToken: cancellationToken);

                throw;
            }
        }

        private async Task SendNode(ITelegramBotClient telegramBotClient, long chatId, SurveyNodeDTO surveyNodeDTO, CancellationToken cancellationToken) => await telegramBotClient.SendMessage(chatId, telegramTextFormatter.Format(surveyNodeDTO), replyMarkup: telegramKeyboardService.Build(surveyNodeDTO.Type, surveyNodeDTO.Options, surveyNodeDTO.Metadata), cancellationToken: cancellationToken);
    }
}
