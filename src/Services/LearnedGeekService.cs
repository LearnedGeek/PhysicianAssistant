using System.Text;

namespace PhysicianAssistant.Services;

public class LearnedGeekService : IMessageService
{
    private readonly IClaudeService _claudeService;
    private readonly IConversationService _conversationService;
    private readonly ILogger<LearnedGeekService> _logger;
    private readonly string _knowledgeBase;

    private const string SystemPrompt = """
        You are the Learned Geek AI assistant — a friendly, professional assistant that helps
        people learn about Learned Geek LLC, its services, and Mark McArthey's work.

        IMPORTANT — FIRST MESSAGE: If this is the first message in the conversation (no
        previous conversation history), begin your response with:
        "Hi! I'm the Learned Geek AI assistant."
        Then answer their question naturally. If they just said hello, introduce yourself
        and ask how you can help. For follow-up messages, skip the intro.

        SMS LENGTH: Keep responses under 450 characters. No emojis. No markdown formatting.

        You can answer questions about:
        - Learned Geek's services (software development, AI consulting, cloud architecture)
        - Mark McArthey's background and expertise
        - Technology stack and capabilities
        - Blog posts — provide the URL when referencing a specific post
        - Projects (API Combat, CrewTrack, Lake Country Spanish, etc.)
        - How to get in touch

        When referencing blog posts, include the full URL so they can read more.

        If someone asks a technical question you can partially answer from the blog content,
        share what you know and point them to the relevant post. If it's beyond what's in
        the knowledge base, suggest they reach out to Mark directly.

        Always include Mark's contact info (markm@learnedgeek.com) in your first response
        and periodically if the conversation continues.

        Detect the language of the message and respond in the same language.
        Be warm, approachable, and professional — that's the Learned Geek voice.
        """;

    public LearnedGeekService(
        IClaudeService claudeService,
        IConversationService conversationService,
        ILogger<LearnedGeekService> logger)
    {
        _claudeService = claudeService;
        _conversationService = conversationService;
        _logger = logger;

        // Load knowledge base from file — check multiple paths for local dev and Azure
        string[] searchPaths =
        [
            Path.Combine(AppContext.BaseDirectory, "Content", "learned-geek-knowledge.md"),
            Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Content", "learned-geek-knowledge.md"),
            Path.Combine("Content", "learned-geek-knowledge.md"),
        ];

        _knowledgeBase = "Learned Geek LLC is a technology consulting company. Contact: markm@learnedgeek.com";

        foreach (var path in searchPaths)
        {
            if (File.Exists(path))
            {
                _knowledgeBase = File.ReadAllText(path);
                _logger.LogInformation("Loaded Learned Geek knowledge base from {Path} ({Length} chars)", path, _knowledgeBase.Length);
                break;
            }
        }

        if (_knowledgeBase.Length < 100)
        {
            _logger.LogWarning("Knowledge base file not found in any search path, using fallback");
        }
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

            var prompt = BuildPrompt(phoneNumber, message);
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

    private string BuildPrompt(string phoneNumber, string message)
    {
        var promptBuilder = new StringBuilder();

        // Add knowledge base context
        promptBuilder.AppendLine("[KNOWLEDGE BASE — use this to answer questions about Learned Geek]");
        promptBuilder.AppendLine(_knowledgeBase);
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
