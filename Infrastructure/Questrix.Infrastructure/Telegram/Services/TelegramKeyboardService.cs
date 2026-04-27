using Questrix.Application.DTOs;
using Questrix.Infrastructure.Telegram.Services.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;

namespace Questrix.Infrastructure.Telegram.Services
{
    public class TelegramKeyboardService : ITelegramKeyboardService
    {
        public ReplyMarkup? Build(IList<SurveyOptionDTO> options, string type)
        {
            if (type != "single_choice")
                return null;

            return new ReplyKeyboardMarkup(options
                .Select(o => new KeyboardButton(o.Label))
                .Select(kb => new[] { kb })
                .ToArray())
            {
                ResizeKeyboard = true
            };
        }
    }
}
