using Questrix.Application.DTOs;
using Telegram.Bot.Types.ReplyMarkups;

namespace Questrix.Infrastructure.Telegram.Services.Interfaces
{
    public interface ITelegramKeyboardService
    {
        ReplyMarkup? Build(IList<SurveyOptionDTO> options, string type);
    }
}
