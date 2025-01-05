using RoomReservation.Application.Utilities.Strategy.Booking;

namespace RoomReservation.Application.Utilities.Factory;

public interface IBookingStrategyFactory
{
    IBookingStrategy GetStrategy(string externalService);
}

internal class BookingStrategyFactory : IBookingStrategyFactory
{
    private readonly IEnumerable<IBookingStrategy> _strategies;

    public BookingStrategyFactory(IEnumerable<IBookingStrategy> strategies)
    {
        _strategies = strategies;
    }

    public IBookingStrategy GetStrategy(string externalService)
    {
        var strategy = _strategies.Where(s => s.ServiceName.Equals(externalService, StringComparison.OrdinalIgnoreCase));
        
        // if the external service is not supported, it will default to DefaultStrategy
        if (strategy.Count() == 0)
        {
            strategy = _strategies.Where(s => s.ServiceName.Equals(string.Empty));
        }

        if (strategy.Count() > 1) 
        {
            throw new InvalidOperationException($"Multiple strategies found for ID: {externalService}");
        }

        return strategy.First();
    }
}
