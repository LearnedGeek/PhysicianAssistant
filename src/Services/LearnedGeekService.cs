using System.Text;

namespace PhysicianAssistant.Services;

public class LearnedGeekService : IMessageService
{
    private readonly IClaudeService _claudeService;
    private readonly IConversationService _conversationService;
    private readonly ILogger<LearnedGeekService> _logger;
    private readonly string _knowledgeBase;

    private const string SystemPrompt = """
        You are the Learned Geek assistant — a friendly, professional AI that helps people
        learn about Learned Geek LLC and its services. You respond via SMS so keep your
        responses concise (under 450 characters). No emojis. No markdown formatting.

        You can answer questions about:
        - Learned Geek's services (software development, AI consulting, cloud architecture)
        - Mark McArthey's background and expertise
        - Technology stack and capabilities
        - How to get in touch

        If someone asks about something outside your knowledge base, politely say you don't
        have that information and suggest they email markm@learnedgeek.com or visit
        learnedgeek.com for more details.

        Be warm, approachable, and professional — that's the Learned Geek voice.
        Detect the language of the message and respond in the same language.
        """;

    public LearnedGeekService(
        IClaudeService claudeService,
        IConversationService conversationService,
        ILogger<LearnedGeekService> logger)
    {
        _claudeService = claudeService;
        _conversationService = conversationService;
        _logger = logger;

        // Load knowledge base from file
        var knowledgePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Content", "learned-geek-knowledge.md");
        if (File.Exists(knowledgePath))
        {
            _knowledgeBase = File.ReadAllText(knowledgePath);
            _logger.LogInformation("Loaded Learned Geek knowledge base ({Length} chars)", _knowledgeBase.Length);
        }
        else
        {
            // Fallback: try relative to content root
            knowledgePath = Path.Combine("Content", "learned-geek-knowledge.md");
            if (File.Exists(knowledgePath))
            {
                _knowledgeBase = File.ReadAllText(knowledgePath);
                _logger.LogInformation("Loaded Learned Geek knowledge base from content root ({Length} chars)", _knowledgeBase.Length);
            }
            else
            {
                _knowledgeBase = "Learned Geek LLC is a technology consulting company. Contact: markm@learnedgeek.com";
                _logger.LogWarning("Knowledge base file not found, using fallback");
            }
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
