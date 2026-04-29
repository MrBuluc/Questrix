namespace Questrix.Application.Exceptions
{
    public class InvitationCodeNotFoundException(string code) : Exception($"There is no {code} invitation code!") { }
    public class SurveyNotFoundException() : Exception("Survey Not Found!") { }
    public class SessionNotFoundException() : Exception("Session Not Found!") { }
    public class SurveyNodeNotFoundException() : Exception("SurveyNode Not Found!") { }
    public class InvitationCodeNotFoundBySurveyIdException(Guid surveyId) : Exception($"Invitation Code Not Found By Survey Id: {surveyId}") { }
}
