using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace SecureFunction
{
    public static class GetData
    {
        [Function("GetData")]
        public static HttpResponseData Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")]
            HttpRequestData req,
            FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger("GetData");
            logger.LogInformation("C# HTTP trigger function processed a request.");

            bool hasRequiredClaim = req.Identities.SelectMany(x => x.Claims).Any(x => x.Type == "roles" && x.Value == "get-master-data");

            if (!hasRequiredClaim)
            {
                HttpResponseData response = req.CreateResponse(HttpStatusCode.Forbidden);
                response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                response.WriteString("Go away");
                return response;
            }
            else
            {
                IEnumerable<string> claims = req.Identities.SelectMany(x => x.Claims).Select(x => $"{x.Type}:{x.Value}");

                HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                response.WriteString($"Claims:{Environment.NewLine}{string.Join(Environment.NewLine, claims)}");

                return response;
            }
        }
    }
}
