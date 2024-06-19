using System.Text.Json.Serialization;

namespace HelloBot.ApiIntegration.ConsoleApp.Dto;

public class Report
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}

public class ReportCollection
{
    [JsonPropertyName("reports")]
    public List<Report> Reports { get; set; }
}


public class ReportData
{
    [JsonPropertyName("rows")]
    public List<Row> Rows { get; set; }

    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }
}

public class Column
{
    [JsonPropertyName("columnName")]
    public string ColumnName { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }
}

public class Row
{
    [JsonPropertyName("columns")]
    public List<Column> Columns { get; set; }
}
