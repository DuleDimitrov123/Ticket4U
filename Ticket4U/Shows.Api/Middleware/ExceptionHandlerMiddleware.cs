using Shows.Application.Exceptions;
using Shows.Domain.Common;
using System.Net;
using System.Text.Json;

namespace Shows.Api.Middleware;

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

        var result = string.Empty;

        switch (exception)
        {
            case DomainException domainException:
                httpStatusCode = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(domainException.ErrorMessages);
                break;
            case NotFoundException:
                httpStatusCode = HttpStatusCode.NotFound;
                break;
            case Exception:
                httpStatusCode = HttpStatusCode.BadRequest;
                break;
        }

        context.Response.StatusCode = (int)httpStatusCode;

        if (result == string.Empty)
        {
            result = JsonSerializer.Serialize(new { error = exception.Message });
        }

        return context.Response.WriteAsync(result);
    }
}
