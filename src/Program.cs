using Polly;
using Polly.Extensions.Http;
using Twilio.TwiML;
using PhysicianAssistant.Configuration;
using PhysicianAssistant.Services;

var builder = WebApplication.CreateBuilder(args);

// Bind configuration
builder.Services.Configure<ClaudeSettings>(builder.Configuration.GetSection(ClaudeSettings.SectionName));
builder.Services.Configure<PubMedSettings>(builder.Configuration.GetSection(PubMedSettings.SectionName));
builder.Services.Configure<ConversationSettings>(builder.Configuration.GetSection(ConversationSettings.SectionName));
builder.Services.Configure<RoutingSettings>(builder.Configuration.GetSection(RoutingSettings.SectionName));

// Polly policies for Claude API
var claudeSettings = builder.Configuration.GetSection(ClaudeSettings.SectionName).Get<ClaudeSettings>() ?? new ClaudeSettings();

var retryPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
        onRetry: (outcome, timespan, retryAttempt, context) =>
        {
            Console.WriteLine($"Retry {retryAttempt} after {timespan.TotalSeconds}s due to {outcome.Exception?.Message ?? outcome.Result?.StatusCode.ToString()}");
        });

var circuitBreakerPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30),
        onBreak: (outcome, timespan) => Console.WriteLine($"Circuit breaker opened for {timespan.TotalSeconds}s"),
        onReset: () => Console.WriteLine("Circuit breaker reset"));

var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(claudeSettings.TimeoutSeconds));

// Register HttpClient for Claude API with Polly
builder.Services.AddHttpClient<IClaudeService, ClaudeService>()
    .AddPolicyHandler(retryPolicy)
    .AddPolicyHandler(circuitBreakerPolicy)
    .AddPolicyHandler(timeoutPolicy);

// Register HttpClient for PubMed (simpler policies — it's fast and free)
builder.Services.AddHttpClient<IPubMedService, PubMedService>()
    .AddPolicyHandler(HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(2, _ => TimeSpan.FromMilliseconds(500)));

// Register services
builder.Services.AddSingleton<IConversationService, FileConversationService>();
builder.Services.AddSingleton<KnowledgeBaseService>();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IContactService, FileContactService>();
builder.Services.AddSingleton<NotificationService>();
builder.Services.AddScoped<ITriageService, TriageService>();
builder.Services.AddScoped<LearnedGeekService>();

var app = builder.Build();

// Health check
app.MapGet("/health", () => Results.Ok(new
{
    status = "healthy",
    service = "Learned Geek Platform",
    timestamp = DateTime.UtcNow
}));

// Admin endpoint — view recent conversations (protected by API key)
app.MapGet("/admin/conversations", (
    HttpRequest request,
    IConversationService conversationService,
    Microsoft.Extensions.Options.IOptions<ConversationSettings> convSettings) =>
{
    var adminKey = app.Configuration["Admin:ApiKey"];
    var providedKey = request.Query["key"].ToString();

    if (string.IsNullOrEmpty(adminKey) || providedKey != adminKey)
        return Results.Unauthorized();

    // Read conversation files from storage
    var storagePath = convSettings.Value.StoragePath;
    if (!Directory.Exists(storagePath))
        return Results.Ok(new { conversations = Array.Empty<object>(), message = "No conversations yet" });

    var conversations = Directory.GetFiles(storagePath, "*.json")
        .OrderByDescending(f => File.GetLastWriteTimeUtc(f))
        .Take(20)
        .Select(f =>
        {
            var phone = Path.GetFileNameWithoutExtension(f);
            var content = File.ReadAllText(f);
            var lastModified = File.GetLastWriteTimeUtc(f);
            return new { phone, lastModified, content };
        })
        .ToList();

    return Results.Ok(new { count = conversations.Count, conversations });
});

// Unified webhook — routes to the correct service based on the Twilio "To" number
app.MapPost("/sms", async (
    HttpRequest request,
    ITriageService triageService,
    LearnedGeekService learnedGeekService,
    NotificationService notificationService,
    Microsoft.Extensions.Options.IOptions<RoutingSettings> routingOptions,
    ILogger<Program> logger) =>
{
    var form = await request.ReadFormAsync();
    var incomingMessage = form["Body"].ToString();
    var fromNumber = form["From"].ToString();
    var toNumber = form["To"].ToString();

    // Determine which service handles this number
    var routing = routingOptions.Value;
    var serviceName = routing.DefaultService;

    // Check if the To number maps to a specific service
    foreach (var mapping in routing.NumberToService)
    {
        if (toNumber.Contains(mapping.Key))
        {
            serviceName = mapping.Value;
            break;
        }
    }

    logger.LogInformation("[{Service}] Message from {From} to {To}: {Message}",
        serviceName, fromNumber, toNumber, incomingMessage);

    // Notify Mark if this is a new conversation (before processing)
    await notificationService.NotifyIfNewConversationAsync(fromNumber, serviceName, incomingMessage);

    // Route to the appropriate service
    IMessageService service = serviceName switch
    {
        "learnedgeek" => learnedGeekService,
        "triage" => triageService,
        _ => triageService
    };

    var responseText = await service.ProcessMessageAsync(fromNumber, incomingMessage);

    // Build TwiML response (same format for SMS and WhatsApp)
    var response = new MessagingResponse();
    response.Message(responseText);

    logger.LogInformation("[{Service}] Response to {Number}: {Response}", serviceName, fromNumber, responseText);

    return Results.Content(response.ToString(), "application/xml");
});

app.Run();
