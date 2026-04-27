using MediatR;
using Microsoft.EntityFrameworkCore;
using Questrix.Application.Bases;
using Questrix.Application.DTOs;
using Questrix.Application.Exceptions;
using Questrix.Application.Interfaces.AutoMapper;
using Questrix.Application.Interfaces.UnitOfWorks;
using Questrix.Domain.Entities;

namespace Questrix.Application.Features.Sessions.Queries.GetCurrentQuestion
{
    public class GetCurrentQuestionSessionQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : BaseHandler(mapper, unitOfWork), IRequestHandler<GetCurrentQuestionSessionQueryRequest, GetCurrentQuestionSessionQueryResponse?>
    {
        public async Task<GetCurrentQuestionSessionQueryResponse?> Handle(GetCurrentQuestionSessionQueryRequest request, CancellationToken cancellationToken)
        {
            Session? session = await unitOfWork.GetReadRepository<Session>().GetAsync(s => s.UserId == request.UserId && s.Status == "Active" && !s.IsDeleted, cancellationToken);

            if (session is null)
                return null;

            SurveyNode currentNode = (await unitOfWork.GetReadRepository<SurveyNode>().GetAsync(sn => sn.Id == session.CurrentNodeId && !sn.IsDeleted, cancellationToken, include: queryable => queryable.Include(sn => sn.Options))) ?? throw new SurveyNodeNotFoundException();

            mapper.Map<SurveyOptionDTO, SurveyOption>(new SurveyOption());

            return mapper.Map<GetCurrentQuestionSessionQueryResponse, SurveyNode>(currentNode);
        }
    }
}
