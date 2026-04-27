using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Questrix.Persistence.Contexts
{
    public class QuestrixDbContextFactory : IDesignTimeDbContextFactory<QuestrixDbContext>
    {
        public QuestrixDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<QuestrixDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);
            return new QuestrixDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
