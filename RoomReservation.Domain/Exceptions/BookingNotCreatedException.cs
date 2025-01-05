namespace RoomReservation.Domain.Exceptions;

public class BookingNotCreatedException(string message) : Exception(message)
{
}
