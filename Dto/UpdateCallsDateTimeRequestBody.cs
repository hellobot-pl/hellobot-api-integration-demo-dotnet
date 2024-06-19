// See https://aka.ms/new-console-template for more information

using System.Text.Json.Serialization;

namespace HelloBot.ApiIntegration.ConsoleApp.Dto;

public class UpdateCallsDateTimeRequestBody
{
    [JsonPropertyName("scenarioId")]
    public Guid ScenarioId { get; set; }

    [JsonPropertyName("ranges")]
    public List<Range> Ranges { get; set; }
}

public class Range
{
    [JsonPropertyName("start")]
    public DateTime Start { get; set; }

    [JsonPropertyName("stop")]
    public DateTime Stop { get; set; }
}