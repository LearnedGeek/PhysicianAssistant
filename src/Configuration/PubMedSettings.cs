namespace PhysicianAssistant.Configuration;

public class PubMedSettings
{
    public const string SectionName = "PubMed";

    public string BaseUrl { get; set; } = "https://eutils.ncbi.nlm.nih.gov/entrez/eutils/";
    public string? ApiKey { get; set; }
    public string? Email { get; set; }
    public string ToolName { get; set; } = "infanzia-triage";
    public int MaxResults { get; set; } = 3;
    public int TimeoutSeconds { get; set; } = 15;
}
