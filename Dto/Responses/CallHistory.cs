// See https://aka.ms/new-console-template for more information

using System.Text.Json.Serialization;

namespace HelloBot.ApiIntegration.ConsoleApp.Dto.Responses;

public class CallHistory
{
    [JsonPropertyName("callId")]
    public string CallId { get; set; }

    [JsonPropertyName("callStartDateTime")]
    public DateTime CallStartDateTime { get; set; }

    [JsonPropertyName("direction")]
    public string Direction { get; set; }

    [JsonPropertyName("calleePhoneNumber")]
    public string CalleePhoneNumber { get; set; }

    [JsonPropertyName("callerPhoneNumber")]
    public string CallerPhoneNumber { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("missedPhoneReason")]
    public string MissedPhoneReason { get; set; }

    [JsonPropertyName("duration")]
    public string Duration { get; set; }

    [JsonPropertyName("recordingUrl")]
    public string RecordingUrl { get; set; }

    [JsonPropertyName("streamUrl")]
    public string StreamUrl { get; set; }

    [JsonPropertyName("transcriptionUrl")]
    public string TranscriptionUrl { get; set; }

    [JsonPropertyName("redirected")]
    public bool Redirected { get; set; }
}