using System.Text;
using System.Text.Json;

namespace PhysicianAssistant.Services;

public class TriageService : ITriageService
{
    private readonly IClaudeService _claudeService;
    private readonly IPubMedService _pubMedService;
    private readonly IConversationService _conversationService;
    private readonly ILogger<TriageService> _logger;

    private const string SystemPrompt = """
        Eres un asistente pediátrico compasivo que ayuda a los padres cuando su médico no está disponible.
        Te comunicas en español.

        Tus ÚNICAS funciones son:
        1. Saludar al padre/madre cálidamente y preguntar sobre el niño (nombre, edad)
        2. Pedirles que describan los síntomas claramente
        3. Evaluar la urgencia basándote ÚNICAMENTE en lo que describen
        4. Si detectas CUALQUIER indicador de emergencia, dirigirlos inmediatamente a servicios de emergencia
           — NO continúes la conversación:
           - Dificultad para respirar / no respira
           - Convulsiones
           - Inconsciente / no responde
           - Sangrado severo
           - Labios azules o decoloración de la piel
           - No se despierta
           - Fiebre alta en bebé menor de 3 meses
        5. Si NO es una emergencia: tranquilizar al padre/madre con calma, confirmar que el doctor
           revisará todo y se comunicará con ellos
        6. Resumir lo capturado antes de cerrar

        NO ERES MÉDICO. No diagnosticas. No recetas.
        Solo capturas información y mantienes a los padres tranquilos.
        Siempre recuerda a los padres que el doctor tomará todas las decisiones clínicas.

        Cuando tengas suficiente información para cerrar la conversación, incluye al final un bloque JSON
        con la evaluación de triaje (el padre no verá esto, es para el sistema):
        ---TRIAGE_JSON---
        {
          "triage_level": "emergency | urgent | non_urgent",
          "child_name": "",
          "child_age": "",
          "symptoms": [],
          "summary": "",
          "recommended_action": ""
        }
        ---END_TRIAGE_JSON---
        """;

    private const string SystemPromptWithContext = """
        Eres un asistente pediátrico compasivo que ayuda a los padres cuando su médico no está disponible.
        Te comunicas en español.

        Tus ÚNICAS funciones son:
        1. Saludar al padre/madre cálidamente y preguntar sobre el niño (nombre, edad)
        2. Pedirles que describan los síntomas claramente
        3. Evaluar la urgencia basándote ÚNICAMENTE en lo que describen
        4. Si detectas CUALQUIER indicador de emergencia, dirigirlos inmediatamente a servicios de emergencia
           — NO continúes la conversación:
           - Dificultad para respirar / no respira
           - Convulsiones
           - Inconsciente / no responde
           - Sangrado severo
           - Labios azules o decoloración de la piel
           - No se despierta
           - Fiebre alta en bebé menor de 3 meses
        5. Si NO es una emergencia: tranquilizar al padre/madre con calma, confirmar que el doctor
           revisará todo y se comunicará con ellos
        6. Resumir lo capturado antes de cerrar

        NO ERES MÉDICO. No diagnosticas. No recetas.
        Solo capturas información y mantienes a los padres tranquilos.
        Siempre recuerda a los padres que el doctor tomará todas las decisiones clínicas.

        Tienes acceso a contexto clínico de PubMed. Úsalo para informar tu comprensión
        pero NUNCA cites artículos ni uses lenguaje técnico con los padres.
        Comunícate en lenguaje simple y tranquilizador apropiado para un padre/madre preocupado(a).

        Cuando tengas suficiente información para cerrar la conversación, incluye al final un bloque JSON
        con la evaluación de triaje (el padre no verá esto, es para el sistema):
        ---TRIAGE_JSON---
        {
          "triage_level": "emergency | urgent | non_urgent",
          "child_name": "",
          "child_age": "",
          "symptoms": [],
          "summary": "",
          "recommended_action": ""
        }
        ---END_TRIAGE_JSON---
        """;

    // Symptom keywords that trigger PubMed lookup
    private static readonly string[] SymptomKeywords =
    [
        "fiebre", "fever", "temperatura",
        "tos", "cough",
        "vómito", "vomit",
        "diarrea", "diarrhea",
        "llanto", "crying", "llora",
        "dolor", "pain",
        "sarpullido", "rash", "erupción",
        "respirar", "breathing", "respira",
        "convulsión", "seizure",
        "no come", "not eating",
        "mocos", "congestion"
    ];

    public TriageService(
        IClaudeService claudeService,
        IPubMedService pubMedService,
        IConversationService conversationService,
        ILogger<TriageService> logger)
    {
        _claudeService = claudeService;
        _pubMedService = pubMedService;
        _conversationService = conversationService;
        _logger = logger;
    }

    public async Task<string> ProcessMessageAsync(string phoneNumber, string message, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Triage message from {PhoneNumber}: {Message}", phoneNumber, message);

        var lowerMessage = message.Trim().ToLowerInvariant();
        if (lowerMessage is "reset" or "clear" or "nuevo")
        {
            _conversationService.ClearConversation(phoneNumber);
            return "Conversación reiniciada. ¿En qué puedo ayudarle?";
        }

        try
        {
            _conversationService.AddMessage(phoneNumber, "Padre/Madre", message);

            var prompt = await BuildPromptAsync(phoneNumber, message, cancellationToken);

            var response = await _claudeService.GenerateAsync(prompt, prompt.Contains("[CONTEXTO CLÍNICO")
                ? SystemPromptWithContext
                : SystemPrompt, cancellationToken);

            // Strip triage JSON from the response before sending to parent
            var parentResponse = StripTriageJson(response);

            _conversationService.AddMessage(phoneNumber, "Asistente", parentResponse);

            _logger.LogInformation("Triage response for {PhoneNumber}: {Response}", phoneNumber, parentResponse);

            return parentResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing triage message from {PhoneNumber}", phoneNumber);
            return "Disculpe, estoy teniendo dificultades en este momento. Si es una emergencia, por favor llame a servicios de emergencia inmediatamente. De lo contrario, intente nuevamente en un momento.";
        }
    }

    private async Task<string> BuildPromptAsync(string phoneNumber, string message, CancellationToken cancellationToken)
    {
        var promptBuilder = new StringBuilder();

        // Extract symptom keywords and fetch PubMed context if found
        var detectedSymptoms = ExtractSymptomKeywords(message);
        if (detectedSymptoms.Length > 0)
        {
            try
            {
                var context = await _pubMedService.GetClinicalContextAsync(detectedSymptoms, cancellationToken);
                if (context.HasResults)
                {
                    promptBuilder.AppendLine("[CONTEXTO CLÍNICO — solo uso interno, no citar directamente al padre/madre]");
                    promptBuilder.AppendLine($"Fuente: {context.Source}");
                    promptBuilder.AppendLine($"PMIDs: {string.Join(", ", context.Pmids)}");
                    promptBuilder.AppendLine($"Resumen de evidencia: {context.Abstracts}");
                    promptBuilder.AppendLine();
                    promptBuilder.AppendLine("Usa esta evidencia para informar tu respuesta pero comunícate en lenguaje");
                    promptBuilder.AppendLine("simple y tranquilizador apropiado para un padre/madre preocupado(a).");
                    promptBuilder.AppendLine();
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "PubMed lookup failed, continuing without clinical context");
            }
        }

        // Add conversation history
        var conversationContext = _conversationService.GetConversationContext(phoneNumber);
        if (!string.IsNullOrEmpty(conversationContext))
        {
            promptBuilder.AppendLine("Conversación previa:");
            promptBuilder.AppendLine(conversationContext);
            promptBuilder.AppendLine();
        }

        promptBuilder.AppendLine($"Padre/Madre: {message}");
        promptBuilder.AppendLine();
        promptBuilder.AppendLine("Asistente:");

        return promptBuilder.ToString();
    }

    private static string[] ExtractSymptomKeywords(string message)
    {
        var lowerMessage = message.ToLowerInvariant();
        return SymptomKeywords
            .Where(keyword => lowerMessage.Contains(keyword))
            .ToArray();
    }

    private static string StripTriageJson(string response)
    {
        var startMarker = "---TRIAGE_JSON---";
        var startIndex = response.IndexOf(startMarker, StringComparison.Ordinal);
        if (startIndex >= 0)
        {
            return response[..startIndex].TrimEnd();
        }
        return response;
    }
}
