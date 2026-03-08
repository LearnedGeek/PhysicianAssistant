using System.Collections.Concurrent;
using System.Text.Json;
using Microsoft.Extensions.Options;
using PhysicianAssistant.Configuration;
using PhysicianAssistant.Models;

namespace PhysicianAssistant.Services;

public class FileConversationService : IConversationService
{
    private readonly ConversationSettings _settings;
    private readonly ILogger<FileConversationService> _logger;
    private readonly string _storagePath;
    private readonly ConcurrentDictionary<string, object> _fileLocks = new();

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    public FileConversationService(
        IOptions<ConversationSettings> settings,
        ILogger<FileConversationService> logger)
    {
        _settings = settings.Value;
        _logger = logger;
        _storagePath = Path.GetFullPath(_settings.StoragePath);
        Directory.CreateDirectory(_storagePath);
        _logger.LogInformation("File conversation storage initialized at {Path}", _storagePath);
    }

    public void AddMessage(string phoneNumber, string role, string content)
    {
        var lockObj = _fileLocks.GetOrAdd(phoneNumber, _ => new object());
        lock (lockObj)
        {
            var history = LoadHistory(phoneNumber);

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

            SaveHistory(phoneNumber, history);

            _logger.LogDebug("Added {Role} message for {PhoneNumber}, history count: {Count}",
                role, phoneNumber, history.Messages.Count);
        }
    }

    public string GetConversationContext(string phoneNumber)
    {
        var lockObj = _fileLocks.GetOrAdd(phoneNumber, _ => new object());
        lock (lockObj)
        {
            var history = LoadHistory(phoneNumber);

            if (history.Messages.Count == 0)
                return string.Empty;

            if (history.LastActivity.AddMinutes(_settings.SessionTimeoutMinutes) < DateTime.UtcNow)
                return string.Empty;

            var context = history.Messages
                .TakeLast(_settings.MaxHistoryPerUser * 2)
                .Select(m => $"{m.Role}: {m.Content}");

            return string.Join("\n", context);
        }
    }

    public void ClearConversation(string phoneNumber)
    {
        var lockObj = _fileLocks.GetOrAdd(phoneNumber, _ => new object());
        lock (lockObj)
        {
            var filePath = GetFilePath(phoneNumber);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                _logger.LogInformation("Cleared conversation for {PhoneNumber}", phoneNumber);
            }
        }
    }

    public bool HasActiveConversation(string phoneNumber)
    {
        var lockObj = _fileLocks.GetOrAdd(phoneNumber, _ => new object());
        lock (lockObj)
        {
            var history = LoadHistory(phoneNumber);
            if (history.Messages.Count == 0)
                return false;

            return history.LastActivity.AddMinutes(_settings.SessionTimeoutMinutes) >= DateTime.UtcNow;
        }
    }

    private ConversationHistory LoadHistory(string phoneNumber)
    {
        var filePath = GetFilePath(phoneNumber);
        if (!File.Exists(filePath))
            return new ConversationHistory();

        try
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<ConversationHistory>(json, JsonOptions) ?? new ConversationHistory();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to load conversation for {PhoneNumber}, starting fresh", phoneNumber);
            return new ConversationHistory();
        }
    }

    private void SaveHistory(string phoneNumber, ConversationHistory history)
    {
        var filePath = GetFilePath(phoneNumber);
        var json = JsonSerializer.Serialize(history, JsonOptions);
        File.WriteAllText(filePath, json);
    }

    private string GetFilePath(string phoneNumber)
    {
        // Sanitize phone number for filename (remove +, spaces, etc.)
        var sanitized = phoneNumber.Replace("+", "").Replace(" ", "").Replace("-", "");
        return Path.Combine(_storagePath, $"{sanitized}.json");
    }
}
