using Questrix.Domain.Entities;

namespace Questrix.Application.Interfaces.Services
{
    public interface ILogService
    {
        Task SaveAsync(ExceptionLog exceptionLog, CancellationToken cancellationToken);
    }
}
