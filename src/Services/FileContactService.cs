using System.Collections.Concurrent;
using System.Text.Json;
using PhysicianAssistant.Models;

namespace PhysicianAssistant.Services;

public class FileContactService : IContactService
{
    private readonly string _storagePath;
    private readonly ILogger<FileContactService> _logger;
    private readonly ConcurrentDictionary<string, object> _fileLocks = new();

    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public FileContactService(ILogger<FileContactService> logger)
    {
        _storagePath = Path.GetFullPath("contacts");
        Directory.CreateDirectory(_storagePath);
        _logger = logger;
        _logger.LogInformation("Contact storage initialized at {Path}", _storagePath);
    }

    public PhoneContact? GetContact(string phoneNumber)
    {
        var lockObj = _fileLocks.GetOrAdd(phoneNumber, _ => new object());
        lock (lockObj)
        {
            var filePath = GetFilePath(phoneNumber);
            if (!File.Exists(filePath))
                return null;

            try
            {
                var json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<PhoneContact>(json, JsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to load contact for {PhoneNumber}", phoneNumber);
                return null;
            }
        }
    }

    public void SaveContact(PhoneContact contact)
    {
        var lockObj = _fileLocks.GetOrAdd(contact.PhoneNumber, _ => new object());
        lock (lockObj)
        {
            contact.LastSeen = DateTime.UtcNow;
            var filePath = GetFilePath(contact.PhoneNumber);
            var json = JsonSerializer.Serialize(contact, JsonOptions);
            File.WriteAllText(filePath, json);
            _logger.LogDebug("Saved contact {PhoneNumber}: {Name}", contact.PhoneNumber, contact.DisplayName ?? "(unnamed)");
        }
    }

    private string GetFilePath(string phoneNumber)
    {
        var sanitized = phoneNumber.Replace("+", "").Replace(" ", "").Replace("-", "");
        return Path.Combine(_storagePath, $"{sanitized}.json");
    }
}
