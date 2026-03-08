namespace PhysicianAssistant.Models;

public class ConversationHistory
{
    public List<ConversationMessage> Messages { get; set; } = [];
    public DateTime LastActivity { get; set; } = DateTime.UtcNow;
}

public record ConversationMessage(string Role, string Content, DateTime Timestamp);
