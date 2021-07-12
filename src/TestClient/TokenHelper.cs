using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace TestClient
{
    public class TokenHelper
    {
        public Task<AuthenticationResult> Test2Async()
        {
            string scope = "api://brandaris-api/.default";
            string clientId = "b187daf6-c16a-4cc2-9ac8-2ff7796f5b28";
            string clientSecret = "2hc.dY-4SbGH7H-Z3udLt-264B8Dsi~Qkd";
            string tenantId = "ae86fed2-d115-4a00-b6ed-68ff87b986f7";
            string authority = $"https://login.microsoftonline.com/{tenantId}";

            IConfidentialClientApplication client = ConfidentialClientApplicationBuilder
                                                   .Create(clientId)
                                                   .WithClientSecret(clientSecret)
                                                   .WithAuthority(new Uri(authority))
                                                   .Build();

            AcquireTokenForClientParameterBuilder request = client.AcquireTokenForClient(new[]
                                                                                         {
                                                                                             scope
                                                                                         });

            return request.ExecuteAsync();
        }
    }
}
