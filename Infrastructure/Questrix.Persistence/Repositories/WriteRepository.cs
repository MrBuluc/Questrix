using Microsoft.EntityFrameworkCore;
using Questrix.Application.Interfaces.Repositories;
using Questrix.Domain.Common;

namespace Questrix.Persistence.Repositories
{
    public class WriteRepository<T>(DbContext dbContext) : IWriteRepository<T> where T : class, IEntityBase, new()
    {
        private readonly DbContext dbContext = dbContext;
        private DbSet<T> Table { get => dbContext.Set<T>(); }

        public async Task<T> AddAsync(T entity, CancellationToken token)
        {
            return (await Table.AddAsync(entity, cancellationToken: token)).Entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await Task.Run(() => Table.Update(entity));
            return entity;
        }
    }
}
