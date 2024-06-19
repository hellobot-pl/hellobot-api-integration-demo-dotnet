// See https://aka.ms/new-console-template for more information

using System.Text.Json.Serialization;

namespace HelloBot.ApiIntegration.ConsoleApp.Dto;

internal class UpdateRepeatPropRequestBody
{
    [JsonPropertyName("scenarioId")]
    public Guid ScenarioId { get; set; }
    [JsonPropertyName("newValue")]
    public int NewValue { get; set; }
}