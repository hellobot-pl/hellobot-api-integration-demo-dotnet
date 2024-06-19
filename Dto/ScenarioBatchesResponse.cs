using System.Text.Json.Serialization;

namespace HelloBot.ApiIntegration.ConsoleApp.Dto;

public class ScenarioBatchesResponse
{
    [JsonPropertyName("batches")]
    public List<Batch> Batches { get; set; }

    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }
}

public class Batch
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("batchName")]
    public string BatchName { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("nextCallDate")]
    public DateTime? NextCallDate { get; set; }

    [JsonPropertyName("recordsCount")]
    public int RecordsCount { get; set; }

    [JsonPropertyName("savedOnDate")]
    public DateTime? SavedOnDate { get; set; }
}
