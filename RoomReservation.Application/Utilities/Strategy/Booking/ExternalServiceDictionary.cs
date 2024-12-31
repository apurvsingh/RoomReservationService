namespace RoomReservation.Application.Utilities.Strategy.Booking
{
    public static class ExternalServiceDictionary
    {
        public static Dictionary<string, IBookingStrategy> ExternalServiceStrategies = new Dictionary<string, IBookingStrategy>()
        {
            {"google",  new GoogleBookingStrategy()},
            {"ms365", new Ms365BookingStrategy() },
            {string.Empty, new DefaultBookingStrategy() }
        };
    }
}
