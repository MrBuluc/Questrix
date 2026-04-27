namespace Questrix.Infrastructure.Telegram.Services.Interfaces
{
    public interface ITelegramBotService
    {
        Task StartAsync(CancellationToken cancellationToken);
    }
}
