namespace Questrix.Application.Features.Sessions.Commands.Start
{
    public class StartSessionCommandResponse
    {
        public Guid Id { get; set; }
        public string FirstQuestion { get; set; }
        public Guid CurrentNodeId { get; set; }
    }
}
