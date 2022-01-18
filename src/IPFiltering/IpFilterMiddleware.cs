using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace IPFiltering;

public class IpFilterMiddleware
{
    private readonly IEnumerable<IPAddress> _ipAddresses;
    private readonly IEnumerable<IPNetwork> _ipNetworks;
    private readonly ILogger<IpFilterMiddleware> _logger;
    private readonly RequestDelegate _next;

    public IpFilterMiddleware(RequestDelegate next, ILogger<IpFilterMiddleware> logger, IpSafeList safeList)
    {
        _ipAddresses = !string.IsNullOrWhiteSpace(safeList.IpAddresses) && safeList.IpAddresses.Split(';').Length > 0
            ? safeList.IpAddresses.Split(';').Select(IPAddress.Parse).ToList()
            : Enumerable.Empty<IPAddress>();

        _ipNetworks = !string.IsNullOrWhiteSpace(safeList.IpNetworks) && safeList.IpNetworks.Split(';').Length > 0
            ? safeList.IpNetworks.Split(';').Select(IPNetwork.Parse).ToList()
            : Enumerable.Empty<IPNetwork>();

        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        Endpoint? endpoint = context.GetEndpoint();
        if (endpoint?.Metadata?.GetMetadata<IExcludeIpFilter>() is object)
        {
            await _next(context);
            return;
        }

        IPAddress remoteIp = context.Connection.RemoteIpAddress ?? throw new ArgumentException("Remote IP is NULL, may due to missing ForwardedHeaders.");

        _logger.LogDebug("Remote IpAddress: {RemoteIp}", remoteIp);

        if (remoteIp.IsIPv4MappedToIPv6)
        {
            remoteIp = remoteIp.MapToIPv4();
        }

        if (!_ipAddresses.Contains(remoteIp) &&
            !_ipNetworks.Any(x => x.Contains(remoteIp)))
        {
            _logger.LogWarning("Forbidden Request from IP: {RemoteIp}", remoteIp);
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return;
        }

        await _next.Invoke(context);
    }
}
