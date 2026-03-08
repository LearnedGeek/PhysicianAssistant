namespace PhysicianAssistant.Configuration;

public class ClaudeSettings
{
    public const string SectionName = "Claude";

    public string ApiKey { get; set; } = string.Empty;
    public string Model { get; set; } = "claude-sonnet-4-6";
    public int MaxTokens { get; set; } = 1024;
    public int TimeoutSeconds { get; set; } = 30;
}
