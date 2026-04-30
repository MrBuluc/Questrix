using Questrix.Application.Interfaces.Services;
using Questrix.Application.Interfaces.UnitOfWorks;
using Questrix.Domain.Entities;

namespace Questrix.Infrastructure.Services
{
    public class LogService(IUnitOfWork unitOfWork) : ILogService
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task SaveAsync(ExceptionLog exceptionLog, CancellationToken cancellationToken)
        {
            exceptionLog.CreatedDate = DateTime.UtcNow;
            await unitOfWork.GetWriteRepository<ExceptionLog>().AddAsync(exceptionLog, cancellationToken);
            await unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
