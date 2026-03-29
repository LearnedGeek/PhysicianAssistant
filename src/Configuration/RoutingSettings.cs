namespace PhysicianAssistant.Configuration;

public class RoutingSettings
{
    public const string SectionName = "Routing";

    public Dictionary<string, string> NumberToService { get; set; } = new();
    public string DefaultService { get; set; } = "triage";
}
