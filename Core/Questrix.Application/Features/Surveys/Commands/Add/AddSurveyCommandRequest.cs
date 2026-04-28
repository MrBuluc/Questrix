using MediatR;
using Questrix.Application.DTOs;

namespace Questrix.Application.Features.Surveys.Commands.Add
{
    public class AddSurveyCommandRequest : IRequest<AddSurveyCommandResponse>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<SurveyNodeDTO> Nodes { get; set; }
        public int InvitationCodeMaxUsege { get; set; }
        public DateTime InvitationCodeExpiresAt { get; set; }
    }
}
