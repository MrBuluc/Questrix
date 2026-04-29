using Questrix.Application.Interfaces.Services;
using Questrix.Application.Models;
using Questrix.Domain.Entities;
using System.Text.Json;

namespace Questrix.Infrastructure.Telegram.Services
{
    public class TelegramTextValidator : ITelegramTextValidator
    {
        public bool ValidateNonTextInput(SurveyNode surveyNode, string answer) => surveyNode.Type switch
        {
            SurveyNodeType.MultipleChoice => surveyNode.Options.Any(o => o.Label == answer),
            SurveyNodeType.LinearScale => int.TryParse(answer, out int value) && IsWithinScale(value, surveyNode.Metadata!),
            _ => true
        };

        private static bool IsWithinScale(int value, string metadata)
        {
            LinearScaleMetadata linearScaleMetadata = JsonSerializer.Deserialize<LinearScaleMetadata>(metadata)!;

            return linearScaleMetadata.Min <= value && value <= linearScaleMetadata.Max;
        }
    }
}
