using MediatR;

namespace Questrix.Application.Features.InvitationCodes.Queries.Resolve
{
    public class ResolveInvitationCodeQueryRequest : IRequest<ResolveInvitationCodeQueryResponse>
    {
        public string Code { get; set; }
    }
}
