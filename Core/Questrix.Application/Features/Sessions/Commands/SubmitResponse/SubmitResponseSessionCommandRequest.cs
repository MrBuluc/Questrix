using MediatR;

namespace Questrix.Application.Features.Sessions.Commands.SubmitResponse
{
    public class SubmitResponseSessionCommandRequest : IRequest<SubmitResponseSessionCommandResponse>
    {
        public string UserId { get; set; }
        public string Answer { get; set; }
    }
}
