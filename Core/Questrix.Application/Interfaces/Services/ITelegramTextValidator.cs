using Questrix.Domain.Entities;

namespace Questrix.Application.Interfaces.Services
{
    public interface ITelegramTextValidator
    {
        bool ValidateNonTextInput(SurveyNode surveyNode, string answer);
    }
}
