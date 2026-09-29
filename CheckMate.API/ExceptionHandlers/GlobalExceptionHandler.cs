using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CheckMate.API.ExceptionHandlers;

public sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger,
    IProblemDetailsService problemDetailsService,
    IHostEnvironment environment) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var requestId = httpContext.TraceIdentifier;

        logger.LogError(
            exception,
            "Unhandled exception occurred. RequestId: {RequestId}",
            requestId);

        const int statusCode =
            StatusCodes.Status500InternalServerError;

        httpContext.Response.StatusCode = statusCode;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = "An unexpected error occurred.",
            Type = "https://www.rfc-editor.org/rfc/rfc9110.html#section-15.6.1",
            Detail = environment.IsDevelopment()
                ? exception.Message
                : null
        };

        return await problemDetailsService.TryWriteAsync(
            new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = problemDetails
            });
    }
}