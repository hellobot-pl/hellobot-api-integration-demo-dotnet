using System.Text.Json.Serialization;

namespace HelloBot.ApiIntegration.ConsoleApp.Dto;

public class NewBatchRecordRequestBody
{
    [JsonPropertyName("scenarioId")]
    public Guid ScenarioId { get; set; }

    [JsonPropertyName("records")]
    public List<BatchRecord> Records { get; set; }
}

public class BatchRecord
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; }

    [JsonPropertyName("parameters")]
    public Dictionary<string, object> Parameters { get; set; }
}
