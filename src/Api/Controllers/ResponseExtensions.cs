using System.Collections;
using Features;
using Microsoft.AspNetCore.Mvc;

namespace Brandaris.Api.Controllers
{
    public static class ResponseExtensions
    {
        public static ActionResult FormatResponse<T>(this ResponseBase<T> response)
        {
            if (response.Value == null)
            {
                return new NotFoundObjectResult(response);
            }

#pragma warning disable CA1508
            if (response.Value is ICollection collection && collection.Count == 0)
            {
                return new NotFoundObjectResult(response);
            }
#pragma warning restore CA1508

            return new OkObjectResult(response);
        }
    }
}
