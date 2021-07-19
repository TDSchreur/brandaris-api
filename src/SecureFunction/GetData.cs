using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace SecureFunction
{
    public static class GetData
    {
        [Function("GetData")]
        public static HttpResponseData Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger("GetData");
            logger.LogInformation("C# HTTP trigger function processed a request.");

            Dictionary<string, StringValues> queryDictionary = QueryHelpers.ParseQuery(req.Url.Query);
            string responseText = queryDictionary.TryGetValue("name", out StringValues name)
                ? $"Welcome to Azure Functions {name}!"
                : "Welcome to Azure Functions John Doe!";

            HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString(responseText);

            return response;
        }
    }
}
