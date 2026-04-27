using Questrix.Domain.Entities;

namespace Questrix.Application.Interfaces.Services
{
    public interface ISurveyExecutionService
    {
        Guid? ResolveNextNode(SurveyNode surveyNode, string answer);
    }
}
