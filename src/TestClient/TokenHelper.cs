using Microsoft.Identity.Client;

namespace TestClient;

public class TokenHelper
{
    private readonly string _authority;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly IMsalHttpClientFactory _httpClientFactory;
    private readonly string _tenantId;
    private IConfidentialClientApplication _client;

    public TokenHelper(IConfiguration configuration, IMsalHttpClientFactory httpClientFactory)
    {
        _clientId = configuration["Authentication:ClientId"];
        _clientSecret = configuration["Authentication:ClientSecret"];
        _tenantId = configuration["Authentication:TenantId"];
        _authority = $"https://login.microsoftonline.com/{_tenantId}";
        _httpClientFactory = httpClientFactory;
    }

    public Task<AuthenticationResult> GetTokens(params string[] scope)
    {
        CreateClient();

        AcquireTokenForClientParameterBuilder request = _client.AcquireTokenForClient(scope);

        return request.ExecuteAsync();
    }

    private void CreateClient()
    {
        if (_client != null)
        {
            return;
        }

        _client = ConfidentialClientApplicationBuilder.Create(_clientId)
                                                      .WithClientSecret(_clientSecret)
                                                      .WithAuthority(new Uri(_authority))
                                                      .WithHttpClientFactory(_httpClientFactory)
                                                      .Build();
    }
}
