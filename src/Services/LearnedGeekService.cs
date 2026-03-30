using System.Text;

namespace PhysicianAssistant.Services;

public class LearnedGeekService : IMessageService
{
    private readonly IClaudeService _claudeService;
    private readonly IConversationService _conversationService;
    private readonly KnowledgeBaseService _knowledgeBaseService;
    private readonly ILogger<LearnedGeekService> _logger;

    private const string SystemPrompt = """
        You are the Learned Geek AI assistant — a friendly, professional assistant that helps
        people learn about Learned Geek LLC, its services, and Mark McArthey's work.

        IMPORTANT — FIRST MESSAGE: If this is the first message in the conversation (no
        previous conversation history), begin your response with:
        "Hi! I'm the Learned Geek AI assistant."
        Then answer their question naturally. Also ask for their name so you can
        personalize the conversation. If they just said hello, introduce yourself,
        ask their name, and ask how you can help. For follow-up messages, skip the intro.
        If they give you their name, use it naturally in responses.

        SMS LENGTH: Keep responses under 450 characters. No emojis. No markdown formatting.

        You can answer questions about:
        - Learned Geek's services (software development, AI consulting, cloud architecture)
        - Mark McArthey's background and expertise
        - Technology stack and capabilities
        - Blog posts — provide the URL when referencing a specific post
        - Projects (API Combat, CrewTrack, Lake Country Spanish, etc.)
        - How to get in touch

        When referencing blog posts, include the full URL so they can read more.

        IMPORTANT — NEVER REJECT A REQUEST FOR HELP. Even if the question is outside the
        knowledge base or outside Learned Geek's core services:
        1. Try to help with general advice first. You have broad technical knowledge — use it.
           A printer issue? Suggest checking WiFi, restarting devices, reinstalling drivers.
           A WordPress problem? Offer basic troubleshooting steps.
        2. Let them know Mark has been notified: "I've flagged this for Mark and he'll
           follow up with you."
        3. Provide contact info so they can reach Mark directly if they want faster help.

        Think of yourself like a helpful receptionist who happens to know a lot about tech.
        You wouldn't tell someone "that's not my department" — you'd try to help and then
        make sure the right person follows up.

        When referencing blog posts, include the full URL so they can read more.

        Mark's contact info:
        - Email: markm@learnedgeek.com
        - Phone: (262) 496-8978
        - Website: learnedgeek.com
        Do NOT give the texting number (205) 898-4335 as contact info — they are already
        texting that number. Give them Mark's direct phone and email instead.
        Include contact info in your first response and periodically if the conversation continues.

        Detect the language of the message and respond in the same language.
        Be warm, approachable, and professional — that's the Learned Geek voice.
        """;

    public LearnedGeekService(
        IClaudeService claudeService,
        IConversationService conversationService,
        KnowledgeBaseService knowledgeBaseService,
        ILogger<LearnedGeekService> logger)
    {
        _claudeService = claudeService;
        _conversationService = conversationService;
        _knowledgeBaseService = knowledgeBaseService;
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
            _conversationService.AddMessage(phoneNumber, "User", message);

            var prompt = await BuildPromptAsync(phoneNumber, message);
            var response = await _claudeService.GenerateAsync(prompt, SystemPrompt, cancellationToken);

            _conversationService.AddMessage(phoneNumber, "Assistant", response);

            _logger.LogInformation("Learned Geek response for {PhoneNumber}: {Response}", phoneNumber, response);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing Learned Geek message from {PhoneNumber}", phoneNumber);
            return "Sorry, I'm having trouble right now. Please email markm@learnedgeek.com or visit learnedgeek.com.";
        }
    }

    private async Task<string> BuildPromptAsync(string phoneNumber, string message)
    {
        var promptBuilder = new StringBuilder();

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
}
