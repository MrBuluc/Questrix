using Questrix.Infrastructure.Telegram.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Questrix.Infrastructure.Telegram.Services
{
    public class TelegramBotService(TelegramOptions telegramOptions, IUpdateHandler updateHandler) : ITelegramBotService
    {
        private readonly IUpdateHandler updateHandler = updateHandler;
        private readonly ITelegramBotClient telegramBotClient = new TelegramBotClient(telegramOptions.BotToken);

        public Task StartAsync(CancellationToken cancellationToken)
        {
            telegramBotClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync, cancellationToken: cancellationToken);

            return Task.CompletedTask;
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken) => await updateHandler.HandleAsync(botClient, update, cancellationToken);

        private static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"TelegramBotService Error: {exception}");

            return Task.CompletedTask;
        }
    }
}
