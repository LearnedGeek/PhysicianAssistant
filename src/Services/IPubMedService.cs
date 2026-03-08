using PhysicianAssistant.Models;

namespace PhysicianAssistant.Services;

public interface IPubMedService
{
    Task<ClinicalContext> GetClinicalContextAsync(string[] symptoms, CancellationToken cancellationToken = default);
}
