using Questrix.Application.DTOs;
using Questrix.Application.Models;
using Questrix.Domain.Entities;
using Questrix.Infrastructure.Telegram.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace Questrix.Infrastructure.Telegram.Services
{
    public class TelegramTextFormatter : ITelegramTextFormatter
    {
        public string Format(SurveyNodeDTO surveyNodeDTO) => surveyNodeDTO.Type switch
        {
            SurveyNodeType.LinearScale => FormatLinearScale(surveyNodeDTO.Question, surveyNodeDTO.Metadata!),
            _ => surveyNodeDTO.Question
        };

        private static string FormatLinearScale(string question, string metadata)
        {
            LinearScaleMetadata linearScaleMetadata = JsonSerializer.Deserialize<LinearScaleMetadata>(metadata)!;

            StringBuilder stringBuilder = new();

            stringBuilder.AppendLine(question);

            if (linearScaleMetadata.MinLabel is not null && linearScaleMetadata.MaxLabel is not null)
            {
                stringBuilder.AppendLine();

                stringBuilder.AppendLine($"⬅️ {linearScaleMetadata.Min} = {linearScaleMetadata.MinLabel}");
                stringBuilder.AppendLine($"➡️ {linearScaleMetadata.Max} = {linearScaleMetadata.MaxLabel}");
            }

            return stringBuilder.ToString();
        }
    }
}
