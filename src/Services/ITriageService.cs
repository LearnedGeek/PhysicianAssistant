namespace PhysicianAssistant.Services;

public interface ITriageService
{
    Task<string> ProcessMessageAsync(string phoneNumber, string message, CancellationToken cancellationToken = default);
}
