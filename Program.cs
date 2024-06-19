using HelloBot.ApiIntegration.ConsoleApp.Authenticator;
using HelloBot.ApiIntegration.ConsoleApp.Dto;
using HelloBot.ApiIntegration.ConsoleApp.Dto.Responses;
using RestSharp;
using Range = HelloBot.ApiIntegration.ConsoleApp.Dto.Range;

const string oauth2Baseurl = "";
const string oauth2Clientid = "";
const string oauth2Clientsecret = "";
const string hellobotbaseurl = "";
const string scenarioid = "";


var restOptions = new RestClientOptions(hellobotbaseurl)
{
    Authenticator = new HellobotAuthenticator(oauth2Baseurl, oauth2Clientid, oauth2Clientsecret),
    BaseUrl = new Uri(hellobotbaseurl)
};

var restClient = new RestClient(restOptions);

try
{
    await FullAsync();
    await ManageDefaultParametersAsync();
    await ReportsAsync();
    await HistoriesAsync();
    await GetRecordingAsync();
}
catch (Exception ex)
{
    Console.WriteLine("An error occurred while executing the API requests: {0}", ex.Message);
}

Console.ReadKey();
return;

async Task FullAsync()
{
    try
    {
        var scenarioTimeZoneResponse = await GetScenarioTimeZoneAsync(restClient, scenarioid);
        var scenarioRequiredParametersResponse = await GetScenarioRequiredParametersAsync(restClient, scenarioid);

        var newScenarioDraftResponse = await CreateNewScenarioDraftAsync(restClient, scenarioid);

        var scenarioBatchesResponse = await GetScenarioBatchesAsync(restClient, scenarioid);

        if (scenarioBatchesResponse != null)
        {
            var draftedBatchId = scenarioBatchesResponse.Batches.FirstOrDefault(b => b.Status == "Drafted")?.Id;
            if (draftedBatchId is not null)
            {
                var newBatchRecordsResponse = await AddBatchRecordsAsync(restClient, scenarioid, draftedBatchId.Value);
                var saveAndStartResponse = await SaveAndStartBatchAsync(restClient, scenarioid, draftedBatchId.Value);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred in FullAsync: {0}", ex.Message);
    }
}

async Task<TimeZoneResponse?> GetScenarioTimeZoneAsync(RestClient client, string scenarioId)
{
    var request = new RestRequest("api/v1/scenario/{scenarioId}/timeZone")
        .AddUrlSegment("scenarioId", scenarioId);

    return await client.GetAsync<TimeZoneResponse>(request);
}

async Task<ScenarioRequiredParametersResponse?> GetScenarioRequiredParametersAsync(RestClient client, string scenarioId)
{
    var request = new RestRequest("api/v1/scenario/{scenarioId}/batch/parameters/required")
        .AddUrlSegment("scenarioId", scenarioId);

    return await client.GetAsync<ScenarioRequiredParametersResponse>(request);
}

async Task<RestResponse> CreateNewScenarioDraftAsync(RestClient client, string scenarioId)
{
    var request = new RestRequest("/api/v1/scenario/{scenarioId}/batch")
        .AddUrlSegment("scenarioId", scenarioId)
        .AddJsonBody(new NewScenarioDraftRequestBody
        {
            Id = Guid.NewGuid(),
            BatchName = "Mój nowy batch",
            ScenarioId = Guid.Parse(scenarioId)
        });

    return await client.ExecutePostAsync(request);
}

async Task<ScenarioBatchesResponse?> GetScenarioBatchesAsync(RestClient client, string scenarioId)
{
    var request = new RestRequest("api/v1/scenario/{scenarioId}/batches")
        .AddUrlSegment("scenarioId", scenarioId)
        .AddQueryParameter("Status", "5")
        .AddQueryParameter("PageNumber", "1")
        .AddQueryParameter("PageSize", "20");

    return await client.GetAsync<ScenarioBatchesResponse>(request);
}

async Task<RestResponse> AddBatchRecordsAsync(RestClient client, string scenarioId, Guid batchId)
{
    var request = new RestRequest("/api/v1/scenario/{scenarioId}/batch/{batchId}/records")
        .AddUrlSegment("scenarioId", scenarioId)
        .AddUrlSegment("batchId", batchId.ToString())
        .AddJsonBody(new NewBatchRecordRequestBody
        {
            ScenarioId = Guid.Parse(scenarioId),
            Records = new List<BatchRecord>
            {
                new BatchRecord
                {
                    Id = Guid.NewGuid(),
                    PhoneNumber = "733035596",
                    Parameters = new Dictionary<string, object>
                    {
                        { "amount", 1000 },
                        { "currency", "PLN" },
                        { "customer_name", "Jan Kowalski" },
                        { "max_date", "2024-06-31" },
                        { "pesel_number", "4050" },
                    }
                }
            }
        });

    return await client.ExecutePostAsync(request);
}

async Task<RestResponse> SaveAndStartBatchAsync(RestClient client, string scenarioId, Guid batchId)
{
    var request = new RestRequest("/api/v1/scenario/{scenarioId}/batch/{batchId}/publish")
        .AddUrlSegment("scenarioId", scenarioId)
        .AddUrlSegment("batchId", batchId.ToString())
        .AddJsonBody(new SaveAndStartRequestBody
        {
            ScenarioId = Guid.Parse(scenarioId),
            BatchName = "Moja ostateczna nazwa"
        });

    return await client.ExecutePostAsync(request);
}

async Task ManageDefaultParametersAsync()
{
    try
    {
        var scenarioDefaultParametersResponse = await GetScenarioDefaultParametersAsync(restClient, scenarioid);
        var newScenarioDraftResponse = await CreateNewScenarioDraftAsync(restClient, scenarioid);
        var scenarioBatchesResponse = await GetScenarioBatchesAsync(restClient, scenarioid);

        var draftedBatchId = scenarioBatchesResponse?.Batches.FirstOrDefault(b => b.Status == "Drafted")?.Id;
        if (draftedBatchId != null)
        {
            var updateScenarioDelayResponse = await UpdateScenarioDelayAsync(restClient, scenarioid, draftedBatchId.Value);
            var updateScenarioRepeatResponse = await UpdateScenarioRepeatAsync(restClient, scenarioid, draftedBatchId.Value);
            var updateScenarioDateTimesResponse = await UpdateScenarioDateTimesAsync(restClient, scenarioid, draftedBatchId.Value);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred in ManageDefaultParametersAsync: {0}", ex.Message);
    }
}

async Task<ScenarioDefaultParametersResponse?> GetScenarioDefaultParametersAsync(RestClient client, string scenarioId)
{
    var request = new RestRequest("api/v1/scenario/{scenarioId}/batch/parameters/default")
        .AddUrlSegment("scenarioId", scenarioId);

    return await client.GetAsync<ScenarioDefaultParametersResponse>(request);
}

async Task<RestResponse> UpdateScenarioDelayAsync(RestClient client, string scenarioId, Guid batchId)
{
    var request = new RestRequest("/api/v1/scenario/{scenarioId}/batch/{batchId}/callschedule/delay")
        .AddUrlSegment("scenarioId", scenarioId)
        .AddUrlSegment("batchId", batchId.ToString())
        .AddJsonBody(new UpdateDelayPropRequestBody
        {
            ScenarioId = Guid.Parse(scenarioId),
            NewValue = 120
        });

    return await client.PatchAsync(request);
}

async Task<RestResponse> UpdateScenarioRepeatAsync(RestClient client, string scenarioId, Guid batchId)
{
    var request = new RestRequest("/api/v1/scenario/{scenarioId}/batch/{batchId}/callschedule/repeat")
        .AddUrlSegment("scenarioId", scenarioId)
        .AddUrlSegment("batchId", batchId.ToString())
        .AddJsonBody(new UpdateRepeatPropRequestBody
        {
            ScenarioId = Guid.Parse(scenarioId),
            NewValue = 5
        });

    return await client.PatchAsync(request);
}

async Task<RestResponse> UpdateScenarioDateTimesAsync(RestClient client, string scenarioId, Guid batchId)
{
    var request = new RestRequest("/api/v1/scenario/{scenarioId}/batch/{batchId}/callschedule/callsdatetime")
        .AddUrlSegment("batchId", batchId.ToString())
        .AddUrlSegment("scenarioId", scenarioId)
        .AddJsonBody(new UpdateCallsDateTimeRequestBody
        {
            ScenarioId = Guid.Parse(scenarioId),
            Ranges =
            [
                new Range
                {
                    Start = DateTime.UtcNow.AddHours(-1),
                    Stop = DateTime.UtcNow.AddHours(1)
                }
            ]
        });

    return await client.PostAsync(request);
}

async Task ReportsAsync()
{
    try
    {
        var scenarioAllReportsResponse = await GetScenarioAllReportsAsync(restClient, scenarioid);

        if (scenarioAllReportsResponse != null)
        {
            var firstReportId = scenarioAllReportsResponse.Reports.FirstOrDefault()?.Id;
            if (firstReportId != null)
            {
                await GetScenarioReportDataAsync(restClient, scenarioid, firstReportId);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred in ReportsAsync: {0}", ex.Message);
    }
}

async Task<ReportCollection?> GetScenarioAllReportsAsync(RestClient client, string scenarioId)
{
    var request = new RestRequest("api/v1/scenario/{scenarioId}/reports")
        .AddUrlSegment("scenarioId", scenarioId);

    return await client.GetAsync<ReportCollection>(request);
}

async Task GetScenarioReportDataAsync(RestClient client, string scenarioId, string reportId)
{
    var request = new RestRequest("api/v1/scenario/{scenarioId}/report/{reportId}")
        .AddUrlSegment("scenarioId", scenarioId)
        .AddUrlSegment("reportId", reportId)
        .AddQueryParameter("PageNumber", "1")
        .AddQueryParameter("PageSize", "5");

    await client.GetAsync<ReportData>(request);
}

async Task HistoriesAsync()
{
    try
    {
        var scenarioCallsHistoryResponse = await GetScenarioCallsHistoryAsync(restClient, scenarioid);

        if (scenarioCallsHistoryResponse != null)
        {
            var firstCallId = scenarioCallsHistoryResponse.CallHistory.FirstOrDefault()?.CallId;
            if (firstCallId != null)
            {
                await GetScenarioCallDetailAsync(restClient, scenarioid, firstCallId);
                await GetScenarioCallTranscriptionAsync(restClient, scenarioid, firstCallId);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred in HistoriesAsync: {0}", ex.Message);
    }
}

async Task<CallsHistoryResponse?> GetScenarioCallsHistoryAsync(RestClient client, string scenarioId)
{
    var request = new RestRequest("/api/v1/scenario/{scenarioId}/calls")
        .AddUrlSegment("scenarioId", scenarioId)
        .AddQueryParameter("PageNumber", "1")
        .AddQueryParameter("PageSize", "5");

    return await client.GetAsync<CallsHistoryResponse>(request);
}

async Task GetScenarioCallDetailAsync(RestClient client, string scenarioId, string callId)
{
    var request = new RestRequest("/api/v1/scenario/{scenarioId}/call/{callId}")
        .AddUrlSegment("scenarioId", scenarioId)
        .AddUrlSegment("callId", callId);

    await client.GetAsync(request);
}

async Task GetScenarioCallTranscriptionAsync(RestClient client, string scenarioId, string callId)
{
    var request = new RestRequest("/api/v1/scenario/{scenarioId}/call/{callId}/transcription")
        .AddUrlSegment("scenarioId", scenarioId)
        .AddUrlSegment("callId", callId);

    await client.GetAsync(request);
}

async Task GetRecordingAsync()
{
    try
    {
        var scenarioCallsHistoryResponse = await GetScenarioCallsHistoryAsync(restClient, scenarioid);

        if (scenarioCallsHistoryResponse != null)
        {
            var firstCallId = scenarioCallsHistoryResponse.CallHistory.FirstOrDefault()?.CallId;
            if (firstCallId != null)
            {
                await GetScenarioCallRecordingAsync(restClient, scenarioid, firstCallId);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred in GetRecordingAsync: {0}", ex.Message);
    }
}

async Task GetScenarioCallRecordingAsync(RestClient client, string scenarioId, string callId)
{
    var request = new RestRequest("/api/v1/scenario/{scenarioId}/call/{callId}/recording")
        .AddUrlSegment("scenarioId", scenarioId)
        .AddUrlSegment("callId", callId);

    await client.ExecuteGetAsync(request);
}
