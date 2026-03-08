namespace PhysicianAssistant.Services;

public interface IClaudeService
{
    Task<string> GenerateAsync(string prompt, string? systemPrompt = null, CancellationToken cancellationToken = default);
}
