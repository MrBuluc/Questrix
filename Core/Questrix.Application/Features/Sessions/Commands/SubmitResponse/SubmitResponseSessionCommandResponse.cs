using Questrix.Application.DTOs;

namespace Questrix.Application.Features.Sessions.Commands.SubmitResponse
{
    public class SubmitResponseSessionCommandResponse
    {
        public bool IsCompleted { get; set; }
        public SurveyNodeDTO SurveyNode { get; set; }
    }
}
