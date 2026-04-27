using Questrix.Application.Exceptions;
using Questrix.Application.Interfaces.Services;
using Questrix.Application.Interfaces.UnitOfWorks;
using Questrix.Domain.Entities;

namespace Questrix.Persistence.Services
{
    public class InvitationService(IUnitOfWork unitOfWork) : IInvitationService
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<Guid> ResolveSurveyIdAsync(string code, CancellationToken cancellationToken)
        {
            InvitationCode invitationCode = (await unitOfWork.GetReadRepository<InvitationCode>().GetAsync(ic => ic.Code == code && !ic.IsDeleted, cancellationToken)) ?? throw new InvitationCodeNotFoundException(code);

            if (invitationCode.UsegeCount > invitationCode.MaxUsege)
                throw new InvitationCodeMaxUsegeException();

            if (invitationCode.ExpiresAt < DateTime.Now)
                throw new InvitationCodeExpiredException();

            return invitationCode.SurveyId;
        }
    }
}
