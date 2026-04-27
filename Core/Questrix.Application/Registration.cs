using Microsoft.Extensions.DependencyInjection;
using Questrix.Application.Exceptions;
using System.Reflection;

namespace Questrix.Application
{
    public static class Registration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<ExceptionMiddleware>();

            Assembly assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            return services;
        }
    }
}
