using RestSharp;
using RestSharp.Authenticators;

namespace HelloBot.ApiIntegration.ConsoleApp.Authenticator;

public class HellobotAuthenticator : AuthenticatorBase
{
    readonly string _baseUrl;
    readonly string _clientId;
    readonly string _clientSecret;

    public HellobotAuthenticator(string baseUrl, string clientId, string clientSecret) : base(string.Empty)
    {
        _baseUrl = baseUrl;
        _clientId = clientId;
        _clientSecret = clientSecret;
    }

    protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
    {
        Token = string.IsNullOrWhiteSpace(Token) ? await GetToken() : Token;
        return new HeaderParameter(KnownHeaders.Authorization, Token);
    }

    async Task<string> GetToken()
    {
        var options = new RestClientOptions(_baseUrl)
        {
            Authenticator = new HttpBasicAuthenticator(_clientId, _clientSecret),
        };
        using var client = new RestClient(options);

        var request = new RestRequest("realms/Voicebot/protocol/openid-connect/token")
            .AddParameter("grant_type", "client_credentials");
        var response = await client.PostAsync<AccessTokenResponse>(request);
        return $"{response!.TokenType} {response!.AccessToken}";
    }
}
