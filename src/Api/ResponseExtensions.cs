using Brandaris.Features;
using Microsoft.AspNetCore.Mvc;

namespace Brandaris.Api;

public static class ResponseExtensions
{
    public static ActionResult FormatResponse<T>(this ResponseBase<T> response)
    {
        return response.Value == null //// || (response.Value is ICollection collection && collection.Count == 0)
            ? new NotFoundObjectResult(response)
            : new OkObjectResult(response);
    }
}