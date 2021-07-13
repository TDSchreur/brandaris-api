using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;

namespace TestClient
{
    public class TokenHelper
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _scope;
        private readonly string _tenantId;

        public TokenHelper(IConfiguration configuration)
        {
            _clientId = configuration["Authentication:ClientId"];
            _clientSecret = configuration["Authentication:ClientSecret"];
            _tenantId = configuration["Authentication:TenantId"];
            _scope = configuration["Authentication:Scope"];
        }

        public Task<AuthenticationResult> GetTokens()
        {
            string authority = $"https://login.microsoftonline.com/{_tenantId}";

            IConfidentialClientApplication client = ConfidentialClientApplicationBuilder
                                                   .Create(_clientId)
                                                   .WithClientSecret(_clientSecret)
                                                   .WithAuthority(new Uri(authority))
                                                   .Build();

            AcquireTokenForClientParameterBuilder request = client.AcquireTokenForClient(new[]
                                                                                         {
                                                                                             _scope
                                                                                         });

            return request.ExecuteAsync();
        }
    }
}
