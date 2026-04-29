using Questrix.Infrastructure.Telegram.Services.Interfaces;

namespace Questrix.Worker
{
    public class TelegramBotWorker(ITelegramBotService telegramBotService) : BackgroundService
    {
        private readonly ITelegramBotService telegramBotService = telegramBotService;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await telegramBotService.StartAsync(stoppingToken);
        }
    }
}
