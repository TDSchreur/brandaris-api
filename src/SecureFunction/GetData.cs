using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace SecureFunction;

public static class GetData
{
    [FunctionName("GetData")]
    public static IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req,
        ILogger logger)
    {
        logger.LogInformation("C# HTTP trigger function processed a request.");

        bool hasRequiredClaim = req.HttpContext.User.HasClaim("roles", "get-master-data");

        if (!hasRequiredClaim)
        {
            logger.LogWarning("Required claim missing.");
            return new UnauthorizedResult();
        }

        IEnumerable<string> claims = req.HttpContext.User.Identities.SelectMany(x => x.Claims)
                                        .Select(x => $"{x.Type}:{x.Value}");

        return new OkObjectResult($"Claims:{Environment.NewLine}{string.Join(Environment.NewLine, claims)}");
    }
}