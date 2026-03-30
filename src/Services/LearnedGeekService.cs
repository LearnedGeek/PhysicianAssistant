using System.Text;

namespace PhysicianAssistant.Services;

public class LearnedGeekService : IMessageService
{
    private readonly IClaudeService _claudeService;
    private readonly IConversationService _conversationService;
    private readonly KnowledgeBaseService _knowledgeBaseService;
    private readonly IContactService _contactService;
    private readonly ILogger<LearnedGeekService> _logger;

    private const string SystemPrompt = """
        You are the Learned Geek AI assistant — a friendly, professional assistant that helps
        people learn about Learned Geek LLC, its services, and Mark McArthey's work.

        IMPORTANT — FIRST MESSAGE: If this is the first message in the conversation (no
        previous conversation history) AND no contact name is known, begin your response with:
        "Hi! I'm the Learned Geek AI assistant."
        Then answer their question naturally and ask for their name.

        If a CONTACT NAME is provided in the prompt, use it naturally: "Hey Kevin! What can
        I help with today?" Do NOT ask for their name again if you already know it.

        SMS LENGTH: Keep responses under 450 characters. No emojis. No markdown formatting.

        You can answer questions about:
        - Learned Geek's services (software development, AI consulting, cloud architecture)
        - Mark McArthey's background and expertise
        - Technology stack and capabilities
        - Blog posts — provide the URL when referencing a specific post
        - Projects (API Combat, CrewTrack, Lake Country Spanish, etc.)
        - How to get in touch

        IMPORTANT — NEVER REJECT A REQUEST FOR HELP. Even if the question is outside the
        knowledge base or outside Learned Geek's core services:
        1. Try to help with general advice first. You have broad technical knowledge — use it.
        2. Let them know Mark has been notified: "I've flagged this for Mark and he'll
           follow up with you."
        3. Provide contact info so they can reach Mark directly if they want faster help.

        Think of yourself like a helpful receptionist who happens to know a lot about tech.

        When referencing blog posts, include the full URL so they can read more.

        Mark's contact info:
        - Email: markm@learnedgeek.com
        - Phone: (262) 496-8978
        - Website: learnedgeek.com
        Do NOT give the texting number (205) 898-4335 as contact info — they are already
        texting that number. Give them Mark's direct phone and email instead.
        Include contact info in your first response and periodically if the conversation continues.

        NAME DETECTION: If the user provides their name (e.g., "I'm Kevin", "My name is Sarah",
        "This is Kevin", "Kevin here"), include a line at the very end of your response:
        ---NAME---
        Kevin
        ---END_NAME---
        The user will NOT see this — it's for the system to save their name.

        If the user corrects their name or says they are someone else (e.g., "Actually it's
        Kevyn with a Y", "This isn't Kevin, it's Sarah"), include the corrected name in the
        NAME block. Acknowledge the correction naturally in your response.

        Detect the language of the message and respond in the same language.
        Be warm, approachable, and professional — that's the Learned Geek voice.
        """;

    public LearnedGeekService(
        IClaudeService claudeService,
        IConversationService conversationService,
        KnowledgeBaseService knowledgeBaseService,
        IContactService contactService,
        ILogger<LearnedGeekService> logger)
    {
        _claudeService = claudeService;
        _conversationService = conversationService;
        _knowledgeBaseService = knowledgeBaseService;
        _contactService = contactService;
        _logger = logger;
    }

    public async Task<string> ProcessMessageAsync(string phoneNumber, string message, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Learned Geek message from {PhoneNumber}: {Message}", phoneNumber, message);

        var lowerMessage = message.Trim().ToLowerInvariant();
        if (lowerMessage is "reset" or "clear")
        {
            _conversationService.ClearConversation(phoneNumber);
            return "Conversation reset. How can I help you learn about Learned Geek?";
        }

        try
        {
            // Load or create contact
            var contact = _contactService.GetContact(phoneNumber);
            var isNewContact = contact == null;
            contact ??= new Models.PhoneContact { PhoneNumber = phoneNumber };

            _conversationService.AddMessage(phoneNumber, "User", message);

            var prompt = await BuildPromptAsync(phoneNumber, message, contact);
            var response = await _claudeService.GenerateAsync(prompt, SystemPrompt, cancellationToken);

            // Extract name if Claude detected one
            var extractedName = ExtractName(response);
            if (extractedName != null)
            {
                var oldName = contact.DisplayName;
                contact.DisplayName = extractedName;
                if (oldName != null && oldName != extractedName)
                {
                    contact.IdentityNotes.Add(new Models.IdentityNote
                    {
                        Note = $"Name changed from '{oldName}' to '{extractedName}'",
                    });
                    _logger.LogInformation("Contact {PhoneNumber} name updated: {Old} → {New}", phoneNumber, oldName, extractedName);
                }
                else
                {
                    _logger.LogInformation("Contact {PhoneNumber} name set: {Name}", phoneNumber, extractedName);
                }
            }

            // Save contact (updates LastSeen)
            _contactService.SaveContact(contact);

            // Strip the name block before sending to user
            var userResponse = StripNameBlock(response);

            _conversationService.AddMessage(phoneNumber, "Assistant", userResponse);

            _logger.LogInformation("Learned Geek response for {PhoneNumber}: {Response}", phoneNumber, userResponse);

            return userResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing Learned Geek message from {PhoneNumber}", phoneNumber);
            return "Sorry, I'm having trouble right now. Please email markm@learnedgeek.com or visit learnedgeek.com.";
        }
    }

    private async Task<string> BuildPromptAsync(string phoneNumber, string message, Models.PhoneContact contact)
    {
        var promptBuilder = new StringBuilder();

        // Inject known identity as a fact (not a memory to be retrieved — per ANI findings)
        if (!string.IsNullOrEmpty(contact.DisplayName))
        {
            promptBuilder.AppendLine($"[CONTACT INFO — this person has texted before]");
            promptBuilder.AppendLine($"Name: {contact.DisplayName}");
            promptBuilder.AppendLine($"First seen: {contact.FirstSeen:yyyy-MM-dd}");
            promptBuilder.AppendLine($"You already know their name — do NOT ask for it again.");
            promptBuilder.AppendLine();
        }

        // Add knowledge base context (static + dynamic RSS)
        var knowledgeBase = await _knowledgeBaseService.GetKnowledgeBaseAsync();
        promptBuilder.AppendLine("[KNOWLEDGE BASE — use this to answer questions about Learned Geek]");
        promptBuilder.AppendLine(knowledgeBase);
        promptBuilder.AppendLine();

        // Add conversation history
        var conversationContext = _conversationService.GetConversationContext(phoneNumber);
        if (!string.IsNullOrEmpty(conversationContext))
        {
            promptBuilder.AppendLine("Previous conversation:");
            promptBuilder.AppendLine(conversationContext);
            promptBuilder.AppendLine();
        }

        promptBuilder.AppendLine($"User: {message}");
        promptBuilder.AppendLine();
        promptBuilder.AppendLine("Assistant:");

        return promptBuilder.ToString();
    }

    private static string? ExtractName(string response)
    {
        var startMarker = "---NAME---";
        var endMarker = "---END_NAME---";
        var startIndex = response.IndexOf(startMarker, StringComparison.Ordinal);
        if (startIndex < 0) return null;

        var nameStart = startIndex + startMarker.Length;
        var endIndex = response.IndexOf(endMarker, nameStart, StringComparison.Ordinal);
        if (endIndex < 0) return null;

        var name = response[nameStart..endIndex].Trim();
        return string.IsNullOrEmpty(name) ? null : name;
    }

    private static string StripNameBlock(string response)
    {
        var startMarker = "---NAME---";
        var startIndex = response.IndexOf(startMarker, StringComparison.Ordinal);
        if (startIndex >= 0)
        {
            return response[..startIndex].TrimEnd();
        }
        return response;
    }
}
