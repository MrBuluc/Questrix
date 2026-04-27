using MediatR;

namespace Questrix.Application.Features.Sessions.Commands.Start
{
    public class StartSessionCommandRequest : IRequest<StartSessionCommandResponse>
    {
        public string UserId { get; set; }
        public string InvitationCode { get; set; }
    }
}
