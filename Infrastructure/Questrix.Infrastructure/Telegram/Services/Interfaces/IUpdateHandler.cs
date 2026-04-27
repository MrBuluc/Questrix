using Telegram.Bot;
using Telegram.Bot.Types;

namespace Questrix.Infrastructure.Telegram.Services.Interfaces
{
    public interface IUpdateHandler
    {
        Task HandleAsync(ITelegramBotClient telegramBotClient, Update update, CancellationToken cancellationToken);
    }
}
