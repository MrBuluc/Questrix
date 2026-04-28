using Questrix.Application.DTOs;

namespace Questrix.Application.Features.Sessions.Commands.Start
{
    public class StartSessionCommandResponse
    {
        public Guid Id { get; set; }
        public SurveyNodeDTO SurveyNode { get; set; }
    }
}
