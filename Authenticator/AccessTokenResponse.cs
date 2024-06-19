using System.Text.Json.Serialization;

namespace HelloBot.ApiIntegration.ConsoleApp.Authenticator;

record AccessTokenResponse
{
    [JsonPropertyName("token_type")]
    public string TokenType { get; init; }
    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; }
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; init; }
}
