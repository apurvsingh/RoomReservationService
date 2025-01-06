using RoomReservation.Application.Dtos.Booking;
using RoomReservation.Domain.Entities;

namespace RoomReservation.Application.Mappers.Booking;

public interface IBookingMapper
{
    BookingDto MapToBookingDto(Domain.Entities.Booking booking);
    BookingDto MapToBookingDto(Domain.Entities.Booking booking, int id);
    IEnumerable<BookingDto> MapToBookingDtoList(IEnumerable<Domain.Entities.Booking> bookings);
    Domain.Entities.Booking MapToEnitiy(int id, BookingRequestDto bookingRequestDto);
}

internal class BookingMapper : IBookingMapper
{
    public BookingDto MapToBookingDto(Domain.Entities.Booking booking)
    {
        return new BookingDto()
        {
            StartTime = booking.StartTime,
            EndTime = booking.EndTime,
            RoomId = booking.RoomId,
            Title = booking.Title,
            ExternalService = booking.ExternalService,
            Created = booking.Id == -1 ? false : true
        };
    }

    public IEnumerable<BookingDto> MapToBookingDtoList(IEnumerable<Domain.Entities.Booking> bookings)
    {
        List<BookingDto> result = new List<BookingDto>();
        
        foreach (var item in bookings)
        {
            result.Add(MapToBookingDto(item));
        }
        return result;
    }

    public Domain.Entities.Booking MapToEnitiy(int id, BookingRequestDto bookingRequestDto)
    {
        return new Domain.Entities.Booking() 
        {
            StartTime = bookingRequestDto.StartTime.ToUniversalTime(),
            EndTime = bookingRequestDto.EndTime.ToUniversalTime(),
            Title = bookingRequestDto.Title,
            RoomId = bookingRequestDto.RoomId,
            ExternalService = bookingRequestDto.ExternalService,
            ClientId = id
        };
    }

    public BookingDto MapToBookingDto(Domain.Entities.Booking booking, int id)
    {
        return new BookingDto()
        {
            StartTime = booking.StartTime.ToUniversalTime(),
            EndTime = booking.EndTime.ToUniversalTime(),
            Title = booking.Title,
            RoomId = booking.RoomId,
            ExternalService = booking.ExternalService,
            Created = id == -1 ? false : true
        };
    }
}
