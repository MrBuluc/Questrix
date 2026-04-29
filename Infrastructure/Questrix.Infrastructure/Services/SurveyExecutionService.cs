using Questrix.Application.Interfaces.Services;
using Questrix.Domain.Entities;

namespace Questrix.Infrastructure.Services
{
    public class SurveyExecutionService : ISurveyExecutionService
    {
        public Guid? ResolveNextNode(SurveyNode surveyNode, string answer)
        {
            if (surveyNode.Rules.Count == 0)
                return null;

            foreach (SurveyRule rule in surveyNode.Rules)
            {
                if (!rule.IsDefault && rule.Condition == $"answer == '{answer}'")
                    return rule.NextNodeId;
            }

            return surveyNode.Rules.First(sr => sr.IsDefault).NextNodeId;
        }
    }
}
