using System.Text;
using System.Xml.Linq;

namespace PhysicianAssistant.Services;

public class KnowledgeBaseService
{
    private readonly ILogger<KnowledgeBaseService> _logger;
    private readonly HttpClient _httpClient;
    private string _knowledgeBase = "";
    private DateTime _lastRefresh = DateTime.MinValue;
    private readonly TimeSpan _refreshInterval = TimeSpan.FromHours(6);
    private readonly SemaphoreSlim _refreshLock = new(1, 1);

    private const string RssFeedUrl = "https://learnedgeek.com/feed.xml";
    private const string StaticKnowledgeFile = "Content/learned-geek-knowledge.md";

    public KnowledgeBaseService(ILogger<KnowledgeBaseService> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.Timeout = TimeSpan.FromSeconds(15);
    }

    public async Task<string> GetKnowledgeBaseAsync()
    {
        if (DateTime.UtcNow - _lastRefresh > _refreshInterval || string.IsNullOrEmpty(_knowledgeBase))
        {
            await RefreshAsync();
        }
        return _knowledgeBase;
    }

    private async Task RefreshAsync()
    {
        if (!await _refreshLock.WaitAsync(0))
            return; // Another refresh is in progress

        try
        {
            var sb = new StringBuilder();

            // Load static knowledge base (services, about, projects, contact)
            var staticContent = LoadStaticContent();
            if (!string.IsNullOrEmpty(staticContent))
            {
                sb.AppendLine(staticContent);
                sb.AppendLine();
            }

            // Fetch dynamic blog content from RSS
            var blogContent = await FetchBlogFromRssAsync();
            if (!string.IsNullOrEmpty(blogContent))
            {
                sb.AppendLine(blogContent);
            }

            _knowledgeBase = sb.ToString();
            _lastRefresh = DateTime.UtcNow;
            _logger.LogInformation("Knowledge base refreshed: {Length} chars (static + {BlogPosts} blog posts from RSS)",
                _knowledgeBase.Length, _lastBlogCount);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to refresh knowledge base, using cached version");
            if (string.IsNullOrEmpty(_knowledgeBase))
            {
                _knowledgeBase = LoadStaticContent()
                    ?? "Learned Geek LLC is a technology consulting company. Contact: markm@learnedgeek.com";
            }
        }
        finally
        {
            _refreshLock.Release();
        }
    }

    private int _lastBlogCount;

    private async Task<string> FetchBlogFromRssAsync()
    {
        try
        {
            _logger.LogInformation("Fetching blog content from RSS feed...");
            var rssXml = await _httpClient.GetStringAsync(RssFeedUrl);
            var doc = XDocument.Parse(rssXml);
            var items = doc.Descendants("item").ToList();

            if (items.Count == 0)
            {
                _logger.LogWarning("RSS feed returned no items");
                return "";
            }

            _lastBlogCount = items.Count;
            var sb = new StringBuilder();
            sb.AppendLine("### Blog Posts (from learnedgeek.com/Blog — updated dynamically from RSS feed)");
            sb.AppendLine();

            foreach (var item in items)
            {
                var title = item.Element("title")?.Value ?? "";
                var link = item.Element("link")?.Value ?? "";
                var description = item.Element("description")?.Value ?? "";
                var categories = item.Elements("category").Select(c => c.Value).ToList();
                var pubDate = item.Element("pubDate")?.Value ?? "";

                sb.AppendLine($"- \"{title}\" — {description}");
                if (categories.Count > 0)
                    sb.AppendLine($"  Tags: {string.Join(", ", categories)}");
                sb.AppendLine($"  URL: {link}");
                sb.AppendLine();
            }

            _logger.LogInformation("Fetched {Count} blog posts from RSS feed", items.Count);
            return sb.ToString();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to fetch RSS feed, blog content will be from static file");
            return "";
        }
    }

    private string? LoadStaticContent()
    {
        string[] searchPaths =
        [
            Path.Combine(AppContext.BaseDirectory, StaticKnowledgeFile),
            Path.Combine(AppContext.BaseDirectory, "..", "..", "..", StaticKnowledgeFile),
            StaticKnowledgeFile,
        ];

        foreach (var path in searchPaths)
        {
            if (File.Exists(path))
            {
                var content = File.ReadAllText(path);
                _logger.LogInformation("Loaded static knowledge base from {Path} ({Length} chars)", path, content.Length);

                // Strip the blog posts section from static file — RSS is the dynamic source now
                var blogHeader = "### Blog Posts";
                var blogIndex = content.IndexOf(blogHeader, StringComparison.Ordinal);
                if (blogIndex >= 0)
                {
                    content = content[..blogIndex].TrimEnd();
                    _logger.LogInformation("Stripped static blog section — using RSS feed instead");
                }

                return content;
            }
        }

        _logger.LogWarning("Static knowledge base file not found");
        return null;
    }
}
