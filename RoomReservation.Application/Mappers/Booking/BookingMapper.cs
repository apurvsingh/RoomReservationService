using RoomReservation.Application.Dtos.Booking;

namespace RoomReservation.Application.Mappers.Booking;

public interface IBookingMapper
{
    BookingDto MapToViewModel(Domain.Entities.Booking booking);
    IEnumerable<BookingDto> MapToViewModelList(IEnumerable<Domain.Entities.Booking> bookings);
    Domain.Entities.Booking MapToEnitiy(int id, BookingRequestDto bookingRequestDto);
    Domain.Entities.Booking MapToEnitiy(BookingRequestDto bookingRequestDto);


}

internal class BookingMapper : IBookingMapper
{
    public BookingDto MapToViewModel(Domain.Entities.Booking booking)
    {
        return new BookingDto()
        {
            StartTime = booking.StartTime,
            EndTime = booking.EndTime,
            RoomId = booking.RoomId,
            Title = booking.Title,
        };
    }

    public IEnumerable<BookingDto> MapToViewModelList(IEnumerable<Domain.Entities.Booking> bookings)
    {
        List<BookingDto> result = new List<BookingDto>();
        
        foreach (var item in bookings)
        {
            result.Add(MapToViewModel(item));
        }
        return result;
    }

    public Domain.Entities.Booking MapToEnitiy(int id, BookingRequestDto bookingRequestDto)
    {
        return new Domain.Entities.Booking() 
        {
            StartTime = bookingRequestDto.StartTime,
            EndTime = bookingRequestDto.EndTime,
            Title = bookingRequestDto.Title,
            RoomId = bookingRequestDto.RoomId,
            ExternalService = bookingRequestDto.ExternalService,
            ClientId = id
        };
    }

    public Domain.Entities.Booking MapToEnitiy(BookingRequestDto bookingRequestDto)
    {
        return new Domain.Entities.Booking()
        {
            StartTime = bookingRequestDto.StartTime,
            EndTime = bookingRequestDto.EndTime,
            Title = bookingRequestDto.Title,
            RoomId = bookingRequestDto.RoomId,
            ExternalService = bookingRequestDto.ExternalService
        };
    }
}
