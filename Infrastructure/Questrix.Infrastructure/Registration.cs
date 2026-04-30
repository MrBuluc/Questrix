using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Questrix.Application.Interfaces.Services;
using Questrix.Infrastructure.Services;
using Questrix.Infrastructure.Telegram;
using Questrix.Infrastructure.Telegram.Services;
using Questrix.Infrastructure.Telegram.Services.Interfaces;

namespace Questrix.Infrastructure
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISurveyExecutionService, SurveyExecutionService>();

            services.Configure<TelegramOptions>(configuration.GetSection("Telegram"));

            services.AddSingleton(sp => sp.GetRequiredService<IOptions<TelegramOptions>>().Value);

            services.AddSingleton<IUpdateHandler, UpdateHandler>();
            services.AddSingleton<ITelegramBotService, TelegramBotService>();
            services.AddSingleton<ITelegramKeyboardService, TelegramKeyboardService>();
            services.AddSingleton<ITelegramTextFormatter, TelegramTextFormatter>();
            services.AddSingleton<ITelegramTextValidator, TelegramTextValidator>();
            services.AddScoped<ILogService, LogService>();

            return services;
        }
    }
}
