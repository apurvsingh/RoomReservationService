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
        
        if (strategy == null)
        {
            throw new InvalidOperationException($"No booking strategy found for ID: {externalService}");
        }

        if (strategy.Count() > 1) 
        {
            throw new InvalidOperationException($"Multiple strategies found for ID: {externalService}");
        }

        return strategy.First();
    }
}
