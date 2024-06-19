using System.Text.Json.Serialization;

namespace HelloBot.ApiIntegration.ConsoleApp.Dto;

public class ScenarioRequiredParametersResponse
{
    [JsonPropertyName("parameters")]
    public Dictionary<string, ScenarioRequiredParameters> Parameters { get; set; }
}
public class ScenarioRequiredParameters
{
    [JsonPropertyName("defaultValue")]
    public string DefaultValue { get; set; }

    [JsonPropertyName("readableName")]
    public string ReadableName { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("required")]
    public bool Required { get; set; }
}
