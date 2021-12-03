using System.Net.Http.Headers;
using Microsoft.Identity.Web;
using TestFrontEnd.Models;

namespace TestFrontEnd.ServiceAgents;

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
            "api://brandaris-api/manage-data"
        };
        string accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(scope);
        using HttpRequestMessage request = new(HttpMethod.Get, $"/api/person/{id}");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        HttpResponseMessage response = await _httpClient.SendAsync(request);
        return await response.Content.ReadFromJsonAsync<GetPersonResponse>();
    }

    public async Task<IEnumerable<KeyValuePair<string, string>>> GetRemoteClaimsAsync()
    {
        string[] scope =
        {
            "api://brandaris-api/manage-data"
        };
        string accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(scope);
        using HttpRequestMessage request = new(HttpMethod.Get, "/api/user");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        HttpResponseMessage response = await _httpClient.SendAsync(request);
        return await response.Content.ReadFromJsonAsync<IEnumerable<KeyValuePair<string, string>>>();
    }
}
