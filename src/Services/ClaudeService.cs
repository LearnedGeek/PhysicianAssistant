using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using PhysicianAssistant.Configuration;

namespace PhysicianAssistant.Services;

public class ClaudeService : IClaudeService
{
    private readonly HttpClient _httpClient;
    private readonly ClaudeSettings _settings;
    private readonly ILogger<ClaudeService> _logger;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public ClaudeService(
        HttpClient httpClient,
        IOptions<ClaudeSettings> settings,
        ILogger<ClaudeService> logger)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
        _logger = logger;

        _httpClient.BaseAddress = new Uri("https://api.anthropic.com/");
        _httpClient.DefaultRequestHeaders.Add("x-api-key", _settings.ApiKey);
        _httpClient.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");
    }

    public async Task<string> GenerateAsync(string prompt, string? systemPrompt = null, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Sending request to Claude API, model: {Model}", _settings.Model);

        var request = new ClaudeRequest
        {
            Model = _settings.Model,
            MaxTokens = _settings.MaxTokens,
            System = systemPrompt,
            Messages = [new ClaudeMessage { Role = "user", Content = prompt }]
        };

        var response = await _httpClient.PostAsJsonAsync(
            "v1/messages",
            request,
            JsonOptions,
            cancellationToken
        );

        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError("Claude API error {StatusCode}: {Body}", response.StatusCode, errorBody);
            throw new HttpRequestException($"Claude API returned {response.StatusCode}: {errorBody}");
        }

        var result = await response.Content.ReadFromJsonAsync<ClaudeResponse>(JsonOptions, cancellationToken);

        if (result is null || result.Content.Length == 0)
        {
            throw new InvalidOperationException("Failed to deserialize Claude response");
        }

        var text = result.Content[0].Text;
        _logger.LogDebug("Claude response received, {Length} chars", text.Length);

        return text;
    }
}

internal class ClaudeRequest
{
    public string Model { get; set; } = string.Empty;
    public int MaxTokens { get; set; }
    public string? System { get; set; }
    public ClaudeMessage[] Messages { get; set; } = [];
}

internal class ClaudeMessage
{
    public string Role { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}

internal class ClaudeResponse
{
    public ClaudeContentBlock[] Content { get; set; } = [];
}

internal class ClaudeContentBlock
{
    public string Type { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
}
