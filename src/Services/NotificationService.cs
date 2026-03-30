using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace PhysicianAssistant.Services;

public class NotificationService
{
    private readonly ILogger<NotificationService> _logger;
    private readonly IConversationService _conversationService;
    private readonly string? _twilioSid;
    private readonly string? _twilioToken;
    private readonly string? _notifyNumber;
    private readonly string? _fromNumber;

    public NotificationService(
        IConfiguration config,
        IConversationService conversationService,
        ILogger<NotificationService> logger)
    {
        _conversationService = conversationService;
        _logger = logger;
        _twilioSid = config["Twilio:AccountSid"];
        _twilioToken = config["Twilio:AuthToken"];
        _notifyNumber = config["Twilio:NotifyNumber"];
        _fromNumber = config["Twilio:FromNumber"];
    }

    public bool IsConfigured =>
        !string.IsNullOrEmpty(_twilioSid) &&
        !string.IsNullOrEmpty(_twilioToken) &&
        !string.IsNullOrEmpty(_notifyNumber) &&
        !string.IsNullOrEmpty(_fromNumber);

    public async Task NotifyIfNewConversationAsync(string fromNumber, string serviceName, string message)
    {
        if (!IsConfigured)
            return;

        // Only notify for new conversations (no existing active session)
        if (_conversationService.HasActiveConversation(fromNumber))
            return;

        try
        {
            Twilio.TwilioClient.Init(_twilioSid, _twilioToken);

            var notifyText = $"[TXT-GEEK Lead] New {serviceName} conversation from {fromNumber}: \"{Truncate(message, 100)}\"";

            await MessageResource.CreateAsync(
                to: new PhoneNumber(_notifyNumber),
                from: new PhoneNumber(_fromNumber),
                body: notifyText
            );

            _logger.LogInformation("Notification sent to {NotifyNumber} for new conversation from {From}",
                _notifyNumber, fromNumber);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to send notification for new conversation from {From}", fromNumber);
        }
    }

    private static string Truncate(string value, int maxLength) =>
        value.Length <= maxLength ? value : value[..maxLength] + "...";
}
