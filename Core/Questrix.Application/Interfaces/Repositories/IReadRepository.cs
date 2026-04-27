using Microsoft.EntityFrameworkCore.Query;
using Questrix.Domain.Common;
using System.Linq.Expressions;

namespace Questrix.Application.Interfaces.Repositories
{
    public interface IReadRepository<T> where T : class, IEntityBase, new()
    {
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken,
            Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null,
            bool enableTracking = false);

        Task<IList<T>> GetAllAsync(CancellationToken cancellationToken,
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            bool enableTracking = false);
    }
}
