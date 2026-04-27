namespace Questrix.Application.Interfaces.Services
{
    public interface IInvitationService
    {
        Task<Guid> ResolveSurveyIdAsync(string code, CancellationToken cancellationToken);
    }
}
