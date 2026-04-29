using Questrix.Application.DTOs;
using Questrix.Domain.Entities;
using Telegram.Bot.Types.ReplyMarkups;

namespace Questrix.Infrastructure.Telegram.Services.Interfaces
{
    public interface ITelegramKeyboardService
    {
        ReplyMarkup? Build(SurveyNodeType type, IList<SurveyOptionDTO>? options, string? metadata);
    }
}
