using System.Net;
using System.Text.Json;

namespace MbdcLocalBudgetsApi.Middlewares;

public sealed class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger _logger;

    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the request");

            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = ErrorResponseFactory.MimeContentType;

                var problemDetails = ErrorResponseFactory.UnknownError(ex.Message);

                var jsonResponse = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}