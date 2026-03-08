namespace PhysicianAssistant.Models;

public record ClinicalContext(
    string Source,
    string[] Pmids,
    string? Abstracts,
    string QueryUsed,
    DateTime RetrievedAt
)
{
    public static ClinicalContext Empty => new(
        "PubMed E-utilities (NCBI/NLM)",
        [],
        null,
        string.Empty,
        DateTime.UtcNow
    );

    public bool HasResults => Pmids.Length > 0 && !string.IsNullOrWhiteSpace(Abstracts);
}
