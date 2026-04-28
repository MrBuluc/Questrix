using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Questrix.Application.Interfaces.Repositories;
using Questrix.Application.Interfaces.Services;
using Questrix.Application.Interfaces.UnitOfWorks;
using Questrix.Persistence.Contexts;
using Questrix.Persistence.Repositories;
using Questrix.Persistence.Services;
using Questrix.Persistence.UnitOfWorks;

namespace Questrix.Persistence
{
    public static class Registration
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<QuestrixDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("PostgreSQL")));

            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IInvitationService, InvitationService>();

            return services;
        }
    }
}
