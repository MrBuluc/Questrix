using MediatR;

namespace Questrix.Application.Features.Sessions.Queries.GetCurrentQuestion
{
    public class GetCurrentQuestionSessionQueryRequest : IRequest<GetCurrentQuestionSessionQueryResponse?>
    {
        public string UserId { get; set; }
    }
}
