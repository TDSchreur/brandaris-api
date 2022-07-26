using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Brandaris.Api;

internal sealed class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, ValidationException exception)
    {
        const int statusCode = StatusCodes.Status400BadRequest;
        Dictionary<string, string[]> errors = exception.Errors
                                                       .GroupBy(x => x.PropertyName)
                                                       .ToDictionary(x => x.Key, x => x.Select(y => y.ErrorMessage)
                                                                                       .ToArray());

        HttpValidationProblemDetails response = new(errors)
        {
            Status = statusCode,
        };

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
