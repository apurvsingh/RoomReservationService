using Microsoft.Extensions.Logging;
using RoomReservation.Application.Dtos.Booking;
using RoomReservation.Application.Mappers.Booking;
using RoomReservation.Application.Utilities.Strategy.Booking;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;

namespace RoomReservation.Application.Services;


public interface IBookingService
{
    Task<IEnumerable<BookingDto>> GetBookings();
    Task<IEnumerable<BookingDto>> GetBookingByClientId (string id, BookingRequestDto bookingRequest);
}

internal class BookingService(
        IBookingRepository bookingRepository,
        IBookingMapper bookingMapper,
        ILogger<BookingService> logger) : IBookingService
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

    public async Task<IEnumerable<BookingDto>> GetBookingByClientId(string id, BookingRequestDto bookingRequestDto)
    {
        int intId = int.Parse(id);
        List<Booking> bookings = null;


        var bookingRequest = bookingMapper.MapToEnitiy(intId, bookingRequestDto);

        if (ExternalServiceDictionary.ExternalServiceStrategies.TryGetValue(bookingRequestDto.ExternalService, out var strategy))
        {
            bookings = await strategy.GetBookings(bookingRequestDto.StartTime, bookingRequestDto.EndTime);
        }

        else
        {
            Console.WriteLine($"Payment strategy with ID '{bookingRequestDto.ExternalService}' not supported.");
        }

        return bookings?.Count > 0  ? bookingMapper.MapToViewModelList(bookings) : Enumerable.Empty<BookingDto>();
    }
}
