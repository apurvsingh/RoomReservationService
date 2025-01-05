using RoomReservation.Domain.Exceptions;

namespace RoomReservation.API.Middleware;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }

        catch (ClientNotFoundException ex)
        {
            logger.LogError(ex, ex.Message);

            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("ClientId is invalid");
        }

        catch (BookingNotCreatedException ex)
        {
            logger.LogError(ex, ex.Message);

            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Something went wrong; Booking could not be created");
        }

        catch (Exception ex) 
        {
            logger.LogError(ex, ex.Message);

            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Something went wrong");
        }
    }
}
