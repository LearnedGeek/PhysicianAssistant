namespace PhysicianAssistant.Configuration;

public class ConversationSettings
{
    public const string SectionName = "Conversation";

    public int MaxHistoryPerUser { get; set; } = 10;
    public int SessionTimeoutMinutes { get; set; } = 30;
}
