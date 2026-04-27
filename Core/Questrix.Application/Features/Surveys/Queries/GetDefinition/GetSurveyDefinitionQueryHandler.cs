using MediatR;
using Questrix.Application.Bases;
using Questrix.Application.Exceptions;
using Questrix.Application.Interfaces.AutoMapper;
using Questrix.Application.Interfaces.UnitOfWorks;
using Questrix.Domain.Entities;

namespace Questrix.Application.Features.Surveys.Queries.GetDefinition
{
    public class GetSurveyDefinitionQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : BaseHandler(mapper, unitOfWork), IRequestHandler<GetSurveyDefinitionQueryRequest, GetSurveyDefinitionQueryResponse>
    {
        public async Task<GetSurveyDefinitionQueryResponse> Handle(GetSurveyDefinitionQueryRequest request, CancellationToken cancellationToken) => mapper.Map<GetSurveyDefinitionQueryResponse, Survey>((await unitOfWork.GetReadRepository<Survey>().GetAsync(s => s.Id == request.Id && !s.IsDeleted, cancellationToken)) ?? throw new SurveyNotFoundException());
    }
}
