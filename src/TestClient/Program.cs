using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

#pragma warning disable CA2000

namespace TestClient
{
    public class Program
    {
        public static async Task Main()
        {
            TokenHelper helper = new();

            AuthenticationResult result = await helper.Test2Async();

            Console.WriteLine(result.AccessToken);

            HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

            HttpRequestMessage request = new(HttpMethod.Get, "https://localhost:5001/api/config");

            HttpResponseMessage response = await httpClient.SendAsync(request);

            Console.WriteLine(response.StatusCode);

            string data = await response.Content.ReadAsStringAsync();

            Console.WriteLine(data);
        }
    }
}
