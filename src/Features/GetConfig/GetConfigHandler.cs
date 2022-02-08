using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Brandaris.Features.GetConfig;

public class GetConfigHandler : IRequestHandler<GetConfigQuery, GetConfigResponse>
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<GetConfigHandler> _logger;

    public GetConfigHandler(IConfiguration configuration,
                            ILogger<GetConfigHandler> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public Task<GetConfigResponse> Handle(GetConfigQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get all configuration values as key/value list");

        IEnumerable<KeyValuePair<string, string>> configValues = _configuration.AsEnumerable()
                                                                               .OrderBy(x => x.Key)
                                                                               .Select(kv => new KeyValuePair<string, string>(kv.Key, kv.Value));
        GetConfigResponse response = new(configValues);

        return Task.FromResult(response);
    }
}
