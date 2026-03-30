namespace PhysicianAssistant.Models;

public class PhoneContact
{
    public string PhoneNumber { get; set; } = "";
    public string? DisplayName { get; set; }
    public string? PreferredLanguage { get; set; }
    public DateTime FirstSeen { get; set; } = DateTime.UtcNow;
    public DateTime LastSeen { get; set; } = DateTime.UtcNow;
    public List<IdentityNote> IdentityNotes { get; set; } = [];
}

public class IdentityNote
{
    public string Note { get; set; } = "";
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
