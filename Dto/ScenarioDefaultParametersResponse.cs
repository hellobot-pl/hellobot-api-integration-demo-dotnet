using System.Text.Json.Serialization;

namespace HelloBot.ApiIntegration.ConsoleApp.Dto;

public class ScenarioDefaultParametersResponse
{
    [JsonPropertyName("defaultParameters")]
    public ScenarioDefaultParameters DefaultParameters { get; set; }
}

public class ScenarioDefaultParameters
{
    [JsonPropertyName("repeat")]
    public int Repeat { get; set; }

    [JsonPropertyName("delay")]
    public int Delay { get; set; }

    [JsonPropertyName("firstAvailableCallsDateTimes")]
    public List<FirstAvailableCallsDateTime> FirstAvailableCallsDateTimes { get; set; }

    [JsonPropertyName("additionalParameters")]
    public Dictionary<string, ScenarioAdditionalParameter> AdditionalParameters { get; set; }

    [JsonPropertyName("editable")]
    public bool Editable { get; set; }

    [JsonPropertyName("timeZoneId")]
    public string TimeZoneId { get; set; }
}

public class ScenarioAdditionalParameter
{
    [JsonPropertyName("defaultValue")]
    public object DefaultValue { get; set; }

    [JsonPropertyName("readableName")]
    public string ReadableName { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("required")]
    public bool Required { get; set; }

    [JsonPropertyName("index")]
    public int Index { get; set; }
}

public class FirstAvailableCallsDateTime
{
    [JsonPropertyName("start")]
    public DateTime Start { get; set; }
    [JsonPropertyName("stop")]
    public DateTime Stop { get; set; }
}
