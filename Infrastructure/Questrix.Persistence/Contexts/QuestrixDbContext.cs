using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Questrix.Domain.Common;
using Questrix.Domain.Entities;
using System.Reflection;

namespace Questrix.Persistence.Contexts
{
    public class QuestrixDbContext : DbContext
    {
        public DbSet<InvitationCode> InvitationCodes { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyNode> SurveyNodes { get; set; }
        public DbSet<SurveyOption> SurveyOptions { get; set; }
        public DbSet<SurveyRule> SurveyRules { get; set; }

        public QuestrixDbContext(DbContextOptions<QuestrixDbContext> dbContextOptions) : base(dbContextOptions) { }

        public QuestrixDbContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (EntityEntry<EntityBase<Guid>> entry in ChangeTracker.Entries<EntityBase<Guid>>())
            {
                _ = entry.State switch
                {
                    EntityState.Added => entry.Entity.CreatedDate = DateTime.Now,
                    EntityState.Modified => entry.Entity.UpdatedDate = DateTime.Now,
                    _ => DateTime.Now
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
