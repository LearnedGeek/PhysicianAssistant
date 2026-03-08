using System.Collections.Concurrent;
using Microsoft.Extensions.Options;
using PhysicianAssistant.Configuration;
using PhysicianAssistant.Models;

namespace PhysicianAssistant.Services;

public class InMemoryConversationService : IConversationService
{
    private readonly ConcurrentDictionary<string, ConversationHistory> _conversations = new();
    private readonly ConversationSettings _settings;
    private readonly ILogger<InMemoryConversationService> _logger;

    public InMemoryConversationService(
        IOptions<ConversationSettings> settings,
        ILogger<InMemoryConversationService> logger)
    {
        _settings = settings.Value;
        _logger = logger;
    }

    public void AddMessage(string phoneNumber, string role, string content)
    {
        var history = _conversations.GetOrAdd(phoneNumber, _ => new ConversationHistory());

        if (history.LastActivity.AddMinutes(_settings.SessionTimeoutMinutes) < DateTime.UtcNow)
        {
            _logger.LogInformation("Session expired for {PhoneNumber}, starting new conversation", phoneNumber);
            history.Messages.Clear();
        }

        history.Messages.Add(new ConversationMessage(role, content, DateTime.UtcNow));
        history.LastActivity = DateTime.UtcNow;

        while (history.Messages.Count > _settings.MaxHistoryPerUser * 2)
        {
            history.Messages.RemoveAt(0);
        }

        _logger.LogDebug("Added {Role} message for {PhoneNumber}, history count: {Count}",
            role, phoneNumber, history.Messages.Count);
    }

    public string GetConversationContext(string phoneNumber)
    {
        if (!_conversations.TryGetValue(phoneNumber, out var history))
        {
            return string.Empty;
        }

        if (history.LastActivity.AddMinutes(_settings.SessionTimeoutMinutes) < DateTime.UtcNow)
        {
            return string.Empty;
        }

        var context = history.Messages
            .TakeLast(_settings.MaxHistoryPerUser * 2)
            .Select(m => $"{m.Role}: {m.Content}");

        return string.Join("\n", context);
    }

    public void ClearConversation(string phoneNumber)
    {
        if (_conversations.TryRemove(phoneNumber, out _))
        {
            _logger.LogInformation("Cleared conversation for {PhoneNumber}", phoneNumber);
        }
    }

    public bool HasActiveConversation(string phoneNumber)
    {
        if (!_conversations.TryGetValue(phoneNumber, out var history))
        {
            return false;
        }

        return history.LastActivity.AddMinutes(_settings.SessionTimeoutMinutes) >= DateTime.UtcNow;
    }
}
