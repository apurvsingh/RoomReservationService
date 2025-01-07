using Microsoft.Extensions.Logging;
using RoomReservation.Application.Dtos.Booking;
using RoomReservation.Application.Mappers.Booking;
using RoomReservation.Application.Services.Redis;
using RoomReservation.Application.Utilities.Factory;
using RoomReservation.Application.Utilities.Strategy.Booking;
using RoomReservation.Common.RabbitMq;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Exceptions;
using RoomReservation.Domain.Repositories;

namespace RoomReservation.Application.Services;


public interface IBookingService
{
    Task<IEnumerable<BookingDto>> GetBookings();
    Task<IEnumerable<BookingDto>> GetBookingsByClientId (string id, BookingRequestDto bookingRequest);
    Task<BookingDto> CreateBooking(string clientId, BookingRequestDto bookingRequestDto);
    void CreateBookingRabbitMq(string clientId, BookingRequestDto bookingRequestDto);
}

public class BookingService(
        IBookingRepository bookingRepository,
        IBookingMapper bookingMapper,
        ILogger<BookingService> logger,
        IBookingStrategyFactory _strategyFactory,
        IRabbitMqConsumer rabbitMqConsumer,
        IRedisCacheService redisCacheService) : IBookingService
{
    
    public async Task<IEnumerable<BookingDto>> GetBookings()
    {
        logger.LogInformation("Getting all bookings");
        var bookings = await bookingRepository.GetAllReservationsAsync();

        if(bookings == null)
        {
            return Enumerable.Empty<BookingDto>();
        }

        return bookingMapper.MapToBookingDtoList(bookings);
    }

    public async Task<IEnumerable<BookingDto>> GetBookingsByClientId(string clientId, BookingRequestDto bookingRequestDto)
    {
        var emptyList = Enumerable.Empty<BookingDto>();
        bool parseSuccessful = int.TryParse(clientId, out int intId);
        
        if(!parseSuccessful)
        {
            throw new ClientNotFoundException($"Client Id: {clientId} does not exist");
        }

        string cacheKey = $"booking:{clientId}";
        var cachedBookings = await redisCacheService.GetAsync<IEnumerable<BookingDto>>(cacheKey);

        if (cachedBookings?.ToList().Count > 0)
        {
            return cachedBookings.ToList();
        }

        List<Booking>? bookings = null;

        Booking bookingRequest = bookingMapper.MapToEnitiy(intId, bookingRequestDto);

        var strategy = _strategyFactory.GetStrategy(bookingRequestDto.ExternalService);

        bookings = await strategy.GetBookings(bookingRequest);

        if (bookings != null && bookings.Count>0) 
        {
            await redisCacheService.SetAsync(cacheKey, bookings);

            return bookingMapper.MapToBookingDtoList(bookings);
        }
        
        return emptyList;
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

        if (result == -1)
        {
            bookingResponse.Validation = new Dtos.Validation()
            {
                Message = $"The room with id {bookingReq.RoomId} is already booked during the requested time period. Please try another room or change the time frame."
            };
        }

        else 
        {
            await redisCacheService.RemoveAsync($"booking:{clientId}");
            bookingResponse = bookingMapper.MapToBookingDto(bookingReq, result);
        }

        return bookingResponse;
    }

    public void CreateBookingRabbitMq(string clientId, BookingRequestDto bookingRequestDto)
    {
        bool parseSuccessful = int.TryParse(clientId, out int intId);

        if (!parseSuccessful)
        {
            throw new ClientNotFoundException($"Client Id: {clientId} does not exist");
        }

        var bookingReq = bookingMapper.MapToEnitiy(intId, bookingRequestDto);

        var strategy = _strategyFactory.GetStrategy("");

        rabbitMqConsumer.StartListening();

        strategy.CreateBookingRabbitMq(clientId, bookingReq);
    }
}
