using Serilog.Context;

namespace CheckMate.API.Middlewares;

public class CorrelationMiddleware : IMiddleware
{
    private const string HeaderName = "X-Correlation-Id";

    public async Task InvokeAsync(
        HttpContext context,
        RequestDelegate next)
    {
        var header = context.Request.Headers[HeaderName];

        string correlationId =
            header.Count == 1 &&
            Guid.TryParse(header[0], out var parsedId)
                ? parsedId.ToString()
                : Guid.NewGuid().ToString();

        context.Response.OnStarting(() =>
        {
            context.Response.Headers[HeaderName] = correlationId;
            return Task.CompletedTask;
        });

        using (LogContext.PushProperty("CorrelationId", correlationId))
        {
            await next(context);
        }
    }
}