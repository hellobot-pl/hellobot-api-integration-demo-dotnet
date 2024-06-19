// See https://aka.ms/new-console-template for more information

using System.Text.Json.Serialization;

namespace HelloBot.ApiIntegration.ConsoleApp.Dto.Responses;

public class CallsHistoryResponse
{
    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }

    [JsonPropertyName("callHistory")]
    public List<CallHistory> CallHistory { get; set; }
}