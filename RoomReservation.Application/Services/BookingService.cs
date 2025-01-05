using Microsoft.Extensions.Logging;
using RoomReservation.Application.Dtos.Booking;
using RoomReservation.Application.Mappers.Booking;
using RoomReservation.Application.Utilities.Factory;
using RoomReservation.Application.Utilities.Strategy.Booking;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Exceptions;
using RoomReservation.Domain.Repositories;

namespace RoomReservation.Application.Services;


public interface IBookingService
{
    Task<IEnumerable<BookingDto>> GetBookings();
    Task<IEnumerable<BookingDto>> GetBookingsByClientId (string id, BookingRequestDto bookingRequest);
    Task<BookingDto> CreateBooking(string clientId, BookingRequestDto bookingRequestDto);
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
            throw new ClientNotFoundException($"Client Id: {clientId} does not exist");
        }

        List<Booking>? bookings = null;
        Booking bookingRequest = bookingMapper.MapToEnitiy(intId, bookingRequestDto);

        var strategy = _strategyFactory.GetStrategy(bookingRequestDto.ExternalService);

        bookings = await strategy.GetBookings(bookingRequest);
        

        return bookings?.Count > 0  ? bookingMapper.MapToViewModelList(bookings) : emptyList;
    }

    public async Task<BookingDto> CreateBooking(string clientId, BookingRequestDto bookingRequestDto)
    {
        bool parseSuccessful = int.TryParse(clientId, out int intId);

        if (!parseSuccessful)
        {
            throw new ClientNotFoundException($"Client Id: {clientId} does not exist");
        }

        var bookingReq = bookingMapper.MapToEnitiy(intId ,bookingRequestDto);

        var strategy = _strategyFactory.GetStrategy(bookingRequestDto.ExternalService);

        int result = await strategy.CreateBooking(clientId, bookingReq);

        var bookingResponse = new BookingDto();
        
        if(result == -1)
        {
            bookingResponse.Validation = new Dtos.Validation()
            {
                Message = $"The room with id {bookingReq.RoomId} is already booked during the requested time period. Please try another room or change the time frame."
            };
        }

        return bookingResponse;
    }
}
