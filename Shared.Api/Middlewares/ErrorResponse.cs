using System.Net;

namespace Shared.Api.Middlewares;

public record ErrorResponse(string RequestEndpoint,
    HttpStatusCode RequestStatusCode,
    string ExceptionType,
    IList<string> ExceptionMessages,
    string ExceptionStackTrace);
