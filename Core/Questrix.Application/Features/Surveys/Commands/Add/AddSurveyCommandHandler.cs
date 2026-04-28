using MediatR;
using Questrix.Application.Bases;
using Questrix.Application.DTOs;
using Questrix.Application.Interfaces.AutoMapper;
using Questrix.Application.Interfaces.UnitOfWorks;
using Questrix.Domain.Entities;

namespace Questrix.Application.Features.Surveys.Commands.Add
{
    public class AddSurveyCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : BaseHandler(mapper, unitOfWork), IRequestHandler<AddSurveyCommandRequest, AddSurveyCommandResponse>
    {
        public async Task<AddSurveyCommandResponse> Handle(AddSurveyCommandRequest request, CancellationToken cancellationToken)
        {
            mapper.Map<SurveyOption, SurveyOptionDTO>(new SurveyOptionDTO());
            mapper.Map<SurveyRule, SurveyRuleDTO>(new SurveyRuleDTO());
            mapper.Map<SurveyNode, SurveyNodeDTO>(new SurveyNodeDTO());

            Survey survey = mapper.Map<Survey, AddSurveyCommandRequest>(request);
            survey.Id = Guid.NewGuid();

            foreach (SurveyNode surveyNode in survey.Nodes)
            {
                surveyNode.SurveyId = survey.Id;

                if (surveyNode.Options.Count > 0)
                {
                    foreach (SurveyOption surveyOption in surveyNode.Options)
                    {
                        surveyOption.NodeId = surveyNode.Id;
                    }
                }

                if (surveyNode.Rules.Count > 0)
                {
                    foreach (SurveyRule surveyRule in surveyNode.Rules)
                    {
                        surveyRule.NodeId = surveyNode.Id;
                    }
                }
            }

            survey.Version = 1;
            survey.StartNode = request.Nodes[0].Id;

            await unitOfWork.GetWriteRepository<Survey>().AddAsync(survey, cancellationToken);

            string invitationCode = Guid.NewGuid().ToString();
            await unitOfWork.GetWriteRepository<InvitationCode>().AddAsync(new()
            {
                Code = invitationCode,
                SurveyId = survey.Id,
                MaxUsege = request.InvitationCodeMaxUsege,
                ExpiresAt = request.InvitationCodeExpiresAt,
            }, cancellationToken);

            await unitOfWork.SaveAsync(cancellationToken);

            return new()
            {
                InvitationCode = invitationCode,
            };
        }
    }
}
