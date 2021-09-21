using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

namespace TestClient
{
    public class ServiceWorker : IHostedService
    {
        private readonly IConfiguration _configuration;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly ILogger<ServiceWorker> _logger;
        private readonly TokenHelper _tokenHelper;
        private readonly HttpClient _httpClient;
        private CancellationTokenSource _cts = default!;
        private Task _executingTask = default!;

        public ServiceWorker(
            IHostApplicationLifetime hostApplicationLifetime,
            TokenHelper tokenHelper,
            IConfiguration configuration,
            IMsalHttpClientFactory msalHttpClientFactory,
            ILogger<ServiceWorker> logger)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _tokenHelper = tokenHelper;
            _httpClient = msalHttpClientFactory.GetHttpClient();
            _configuration = configuration;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting worker");

            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            Task.Run(() => _executingTask = ExecuteAsync(_cts.Token), cancellationToken);

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping worker");

            if (_executingTask == null)
            {
                return;
            }

            _cts.Cancel();
            await Task.WhenAny(_executingTask, Task.Delay(-1, cancellationToken));

            cancellationToken.ThrowIfCancellationRequested();
        }

        private async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string scope = _configuration["Authentication:Scope"];
            AuthenticationResult authenticationResult = await _tokenHelper.GetTokens(scope);
            _logger.LogInformation("AccessToken: {AccessToken}", authenticationResult.AccessToken);

            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + authenticationResult.AccessToken);

            using (HttpRequestMessage request = new(HttpMethod.Get, "https://localhost:5001/api/config"))
            {
                HttpResponseMessage response = await httpClient.SendAsync(request, stoppingToken);
                _logger.LogInformation("StatusCode: {StatusCode}", response.StatusCode);

                string data = await response.Content.ReadAsStringAsync(stoppingToken);
                _logger.LogInformation("Content: {Content}", data);
            }

            using (HttpRequestMessage request = new(HttpMethod.Get, "https://localhost:5001/api/person"))
            {
                HttpResponseMessage response = await httpClient.SendAsync(request, stoppingToken);
                _logger.LogInformation("StatusCode: {StatusCode}", response.StatusCode);

                string data = await response.Content.ReadAsStringAsync(stoppingToken);
                _logger.LogInformation("Content: {Content}", data);
            }

            ////string scope = "https://nta7tp2n6crj4.azurewebsites.net/.default";
            ////AuthenticationResult authenticationResult = await _tokenHelper.GetTokens(scope);

            ////_logger.LogInformation("AccessToken: {AccessToken}", authenticationResult.AccessToken);

            ////using (HttpRequestMessage request = new(HttpMethod.Get, "https://nta7tp2n6crj4.azurewebsites.net/api/GetData"))
            ////{
            ////    request.Headers.Add("Authorization", "Bearer " + authenticationResult.AccessToken);

            ////    HttpResponseMessage response = await _httpClient.SendAsync(request, stoppingToken);
            ////    _logger.LogInformation("StatusCode: {StatusCode}", response.StatusCode);

            ////    string data = await response.Content.ReadAsStringAsync(stoppingToken);
            ////    _logger.LogInformation("Content: {Content}", data);
            ////}

            _hostApplicationLifetime.StopApplication();
        }
    }
}
