using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace MbdcLocalBudgetsInfrastructure.Helpers;

public static class ErrorResponseFactory
{
    public const string MimeContentType = "application/problem+json";

    public static ProblemDetails UnknownError(string cause) => new()
    {
        Type = $"https://httpstatuses.com/{(int)HttpStatusCode.InternalServerError}",
        Title = "Internal Server Error",
        Status = (int)HttpStatusCode.InternalServerError,
        Detail = cause
    };

    public static ProblemDetails BadRequest(string cause = "Bad Request") => new()
    {
        Type = $"https://httpstatuses.com/{(int)HttpStatusCode.BadRequest}",
        Title = "Bad Request",
        Status = (int)HttpStatusCode.BadRequest,
        Detail = cause,
    };
}