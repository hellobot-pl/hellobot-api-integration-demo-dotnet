// See https://aka.ms/new-console-template for more information

namespace HelloBot.ApiIntegration.ConsoleApp.Dto;

public class SaveAndStartRequestBody
{
    public Guid ScenarioId { get; set; }
    public string BatchName { get; set; }
}