using System;
using System.Net.Http;
using Microsoft.Identity.Client;

namespace TestClient;

public class MsalHttpClientFactory : IMsalHttpClientFactory, IDisposable
{
    private bool _disposedValue;
    private HttpClient _httpClient;
    private HttpClientHandler _httpClientHandler;

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public HttpClient GetHttpClient()
    {
        Initialize();
        return _httpClient;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _httpClientHandler?.Dispose();
                _httpClient?.Dispose();
            }

            _disposedValue = true;
        }
    }

    private void Initialize()
    {
        if (_httpClient == null)
        {
            // var proxy = new WebProxy { BypassProxyOnLocal = false, Address = new Uri("http://localhost:8001") };
#pragma warning disable CA5399
#pragma warning disable CA5400
            _httpClientHandler = new HttpClientHandler
            {
                // Proxy = proxy,
                // CheckCertificateRevocationList = false,
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            _httpClient = new HttpClient(_httpClientHandler);
        }
    }
}