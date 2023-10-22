using Microsoft.AspNetCore.Http;
using Shared.Application.Exceptions;
using Shared.Domain;
using System.Net;
using System.Text.Json;

namespace Shared.Api.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await ConvertException(context, ex);
        }
    }

    private Task ConvertException(HttpContext context, Exception exception)
    {
        HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

        context.Response.ContentType = "application/json";

        IList<string> customExceptionMessages = null;

        switch (exception)
        {
            case DomainException domainException:
                httpStatusCode = HttpStatusCode.BadRequest;
                customExceptionMessages = domainException.ErrorMessages;
                break;
            case NotFoundException:
                httpStatusCode = HttpStatusCode.NotFound;
                break;
            case Exception:
                httpStatusCode = HttpStatusCode.BadRequest;
                break;
        }

        context.Response.StatusCode = (int)httpStatusCode;

        var result = JsonSerializer.Serialize(
            new ErrorResponse(context.Request.Path,
                httpStatusCode,
                exception.GetType().Name,
                customExceptionMessages is null
                ? new List<string>() { exception.Message }
                : customExceptionMessages,
                exception.ToString()));

        return context.Response.WriteAsync(result);
    }
}
