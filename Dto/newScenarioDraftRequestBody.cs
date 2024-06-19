using System.Text.Json.Serialization;

namespace HelloBot.ApiIntegration.ConsoleApp.Dto;

public class NewScenarioDraftRequestBody
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("batchName")]
    public string BatchName { get; set; }

    [JsonPropertyName("scenarioId")]
    public Guid ScenarioId { get; set; }

}
