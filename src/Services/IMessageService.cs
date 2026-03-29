namespace PhysicianAssistant.Services;

public interface IMessageService
{
    Task<string> ProcessMessageAsync(string phoneNumber, string message, CancellationToken cancellationToken = default);
}
