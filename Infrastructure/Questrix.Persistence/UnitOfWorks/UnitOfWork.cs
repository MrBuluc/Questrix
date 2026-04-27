using Questrix.Application.Interfaces.Repositories;
using Questrix.Application.Interfaces.UnitOfWorks;
using Questrix.Persistence.Contexts;
using Questrix.Persistence.Repositories;

namespace Questrix.Persistence.UnitOfWorks
{
    public class UnitOfWork(QuestrixDbContext dbContext) : IUnitOfWork
    {
        private readonly QuestrixDbContext dbContext = dbContext;

        public async ValueTask DisposeAsync() => await dbContext.DisposeAsync();

        public int Save() => dbContext.SaveChanges();

        public async Task<int> SaveAsync(CancellationToken cancellationToken) => await dbContext.SaveChangesAsync(cancellationToken);

        IReadRepository<T> IUnitOfWork.GetReadRepository<T>() => new ReadRepository<T>(dbContext);

        IWriteRepository<T> IUnitOfWork.GetWriteRepository<T>() => new WriteRepository<T>(dbContext);
    }
}
