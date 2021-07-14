using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Identity.Web;
using TestFrontEnd.Models;

namespace TestFrontEnd.ServiceAgents
{
    public class BrandarisApiServiceAgent : IBrandarisApiServiceAgent
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenAcquisition _tokenAcquisition;

        public BrandarisApiServiceAgent(
            ITokenAcquisition tokenAcquisition,
            HttpClient httpClient)
        {
            _tokenAcquisition = tokenAcquisition;
            _httpClient = httpClient;
        }

        public async Task<GetPersonResponse> GetPersonAsync(int id)
        {
            string[] scope =
            {
                "api://brandaris-api/get-person"
            };
            string accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(scope);
            using HttpRequestMessage request = new(HttpMethod.Get, $"/api/person/{id}");

            // Add token to the Authorization header and make the request
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetPersonResponse>();
        }
    }
}
