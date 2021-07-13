using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

namespace TestClient
{
    public class ServiceWorker : IHostedService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly ILogger<ServiceWorker> _logger;
        private readonly TokenHelper _tokenHelper;
        private CancellationTokenSource _cts = default!;
        private Task _executingTask = default!;

        public ServiceWorker(
            IHostApplicationLifetime hostApplicationLifetime,
            TokenHelper tokenHelper,
            ILogger<ServiceWorker> logger)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _tokenHelper = tokenHelper;
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
            AuthenticationResult result = await _tokenHelper.GetTokens();
            _logger.LogInformation("AccessToken: {AccessToken}", result.AccessToken);

            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);

            using HttpRequestMessage request = new(HttpMethod.Get, "https://localhost:5001/api/config");
            HttpResponseMessage response = await httpClient.SendAsync(request, stoppingToken);
            _logger.LogInformation("StatusCode: {StatusCode}", response.StatusCode);

            string data = await response.Content.ReadAsStringAsync(stoppingToken);
            _logger.LogInformation("Content: {Content}", data);

            _hostApplicationLifetime.StopApplication();
        }
    }
}
