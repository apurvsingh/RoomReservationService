
using System.Diagnostics;

namespace RoomReservation.API.Middleware;

public class TimingHandlerMiddleware(ILogger<TimingHandlerMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        
        try 
        {
            await next.Invoke(context);
            stopwatch.Stop();
        }

        finally
        {
            if (stopwatch.ElapsedMilliseconds / 1000 > 3)
            {
                logger.LogInformation("Request [{Verb}] at {Path} took {Time} ms",
                    context.Request.Method,
                    context.Request.Path,
                    stopwatch.ElapsedMilliseconds);
            }
        }
    }
}
