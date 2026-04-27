using Microsoft.Extensions.DependencyInjection;
using Questrix.Application.Interfaces.AutoMapper;

namespace Questrix.Mapper
{
    public static class Registration
    {
        public static void AddCustomMapper(this IServiceCollection services)
        {
            services.AddSingleton<IMapper, AutoMapper.Mapper>();
        }
    }
}
