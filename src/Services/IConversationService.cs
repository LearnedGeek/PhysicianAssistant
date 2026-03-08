namespace PhysicianAssistant.Services;

public interface IConversationService
{
    void AddMessage(string phoneNumber, string role, string content);
    string GetConversationContext(string phoneNumber);
    void ClearConversation(string phoneNumber);
    bool HasActiveConversation(string phoneNumber);
}
