using Microsoft.Extensions.DependencyInjection;
using Questrix.Application.Interfaces.Services;
using Questrix.Domain.Entities;
using Questrix.Infrastructure.Telegram.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Questrix.Infrastructure.Telegram.Services
{
    public class TelegramBotService(TelegramOptions telegramOptions, IUpdateHandler updateHandler, IServiceScopeFactory serviceScopeFactory) : ITelegramBotService
    {
        private readonly IUpdateHandler updateHandler = updateHandler;
        private readonly IServiceScopeFactory serviceScopeFactory = serviceScopeFactory;
        private readonly ITelegramBotClient telegramBotClient = new TelegramBotClient(telegramOptions.BotToken);

        public Task StartAsync(CancellationToken cancellationToken)
        {
            telegramBotClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiverOptions: new()
            {
                AllowedUpdates = []
            }, cancellationToken: cancellationToken);

            return Task.CompletedTask;
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken) => await updateHandler.HandleAsync(botClient, update, cancellationToken);

        private async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            await using AsyncServiceScope asyncServiceScope = serviceScopeFactory.CreateAsyncScope();

            await asyncServiceScope.ServiceProvider.GetRequiredService<ILogService>().SaveAsync(ExceptionLog.Cast(exception), cancellationToken);
        }
    }
}
