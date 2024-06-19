using System.Text.Json.Serialization;

namespace HelloBot.ApiIntegration.ConsoleApp.Dto.Responses;

public class TimeZoneResponse
{
    [JsonPropertyName("timeZone")]
    public TimeZoneInfo TimeZone { get; set; }

    [JsonPropertyName("ianaTimeZoneId")]
    public string IanaTimeZoneId { get; set; }
}

public class TimeZoneInfo
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("hasIanaId")]
    public bool HasIanaId { get; set; }

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; }

    [JsonPropertyName("standardName")]
    public string StandardName { get; set; }

    [JsonPropertyName("daylightName")]
    public string DaylightName { get; set; }

    [JsonPropertyName("baseUtcOffset")]
    public string BaseUtcOffset { get; set; }

    [JsonPropertyName("supportsDaylightSavingTime")]
    public bool SupportsDaylightSavingTime { get; set; }
}
