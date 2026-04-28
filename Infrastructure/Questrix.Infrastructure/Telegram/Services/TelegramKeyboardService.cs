using Questrix.Application.DTOs;
using Questrix.Application.Models;
using Questrix.Domain.Entities;
using Questrix.Infrastructure.Telegram.Services.Interfaces;
using System.Text.Json;
using Telegram.Bot.Types.ReplyMarkups;

namespace Questrix.Infrastructure.Telegram.Services
{
    public class TelegramKeyboardService : ITelegramKeyboardService
    {
        public ReplyKeyboardMarkup? Build(SurveyNodeType type, IList<SurveyOptionDTO>? options, string? metadata) => type switch
        {
            SurveyNodeType.MultipleChoice => BuildMultipleChoice(options!),
            SurveyNodeType.LinearScale => BuildLinearScale(metadata!),
            _ => null, // ShortAnswer → no keyboard
        };

        private static ReplyKeyboardMarkup BuildMultipleChoice(IList<SurveyOptionDTO> options) => new(options
                .Select(o => new KeyboardButton(o.Label))
                .Select(kb => new[] { kb })
                .ToArray())
        {
            ResizeKeyboard = true
        };

        private static ReplyKeyboardMarkup BuildLinearScale(string metadata)
        {
            LinearScaleMetadata linearScaleMetadata = JsonSerializer.Deserialize<LinearScaleMetadata>(metadata)!;

            return new ReplyKeyboardMarkup(Enumerable.Range(linearScaleMetadata.Min, linearScaleMetadata.Max - linearScaleMetadata.Min + 1)
                .Select(v => new KeyboardButton(v.ToString()))
                .Select(kb => new[] { kb })
                .ToArray())
            {
                ResizeKeyboard = true
            };
        }
    }
}
