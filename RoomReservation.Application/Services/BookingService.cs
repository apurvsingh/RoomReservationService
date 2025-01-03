using Microsoft.Extensions.Logging;
using RoomReservation.Application.Dtos.Booking;
using RoomReservation.Application.Mappers.Booking;
using RoomReservation.Application.Utilities.Factory;
using RoomReservation.Application.Utilities.Strategy.Booking;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;

namespace RoomReservation.Application.Services;


public interface IBookingService
{
    Task<IEnumerable<BookingDto>> GetBookings();
    Task<IEnumerable<BookingDto>> GetBookingsByClientId (string id, BookingRequestDto bookingRequest);
}

public class BookingService(
        IBookingRepository bookingRepository,
        IBookingMapper bookingMapper,
        ILogger<BookingService> logger,
        IBookingStrategyFactory _strategyFactory) : IBookingService
{
    public async Task<IEnumerable<BookingDto>> GetBookings()
    {
        logger.LogInformation("Getting all bookings");
        var bookings = await bookingRepository.GetAllReservationsAsync();

        if(bookings == null)
        {
            return Enumerable.Empty<BookingDto>();
        }

        return bookingMapper.MapToViewModelList(bookings);
    }

    public async Task<IEnumerable<BookingDto>> GetBookingsByClientId(string clientId, BookingRequestDto bookingRequestDto)
    {
        var emptyList = Enumerable.Empty<BookingDto>();
        bool parseSuccessful = int.TryParse(clientId, out int intId);
        
        if(!parseSuccessful)
        {
            return emptyList;
        }

        List<Booking>? bookings = null;
        Booking bookingRequest = bookingMapper.MapToEnitiy(intId, bookingRequestDto);

        var strategy = _strategyFactory.GetStrategy(bookingRequestDto.ExternalService);

        bookings = await strategy.GetBookings(bookingRequest);
        

        return bookings?.Count > 0  ? bookingMapper.MapToViewModelList(bookings) : emptyList;
    }
}
