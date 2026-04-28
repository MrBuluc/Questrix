using Questrix.Application.DTOs;

namespace Questrix.Infrastructure.Telegram.Services.Interfaces
{
    public interface ITelegramTextFormatter
    {
        string Format(SurveyNodeDTO surveyNodeDTO);
    }
}
