namespace RoomReservation.Application.Utilities.Strategy.Booking
{
    public static class ExternalServiceDictionary
    {
        //public static Dictionary<string, IBookingStrategy> ExternalServiceStrategies = new Dictionary<string, IBookingStrategy>()
        //{
        //    {"google",  new GoogleBookingStrategy()},
        //    {"ms365", new Ms365BookingStrategy() },
        //    {"temp", new DefaultBookingStrategy() }
        //};

        public static List<IBookingStrategy> ExternalServiceStrategies = new List<IBookingStrategy>()
        {
             //new GoogleBookingStrategy(),
             //new Ms365BookingStrategy(),
             //new DefaultBookingStrategy()
        };
    }
}
