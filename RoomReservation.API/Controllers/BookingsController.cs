﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RoomReservation.Application.Dtos.Booking;
using RoomReservation.Application.Services;

namespace RoomReservation.API.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingsController(IBookingService bookingService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            var bookings = await bookingService.GetBookings();

            if (bookings == null)
            {
                return NoContent();
            }

            return Ok(bookings);
        }

        [HttpGet]
        [Route("getreservations")]
        public async Task<IActionResult> GetBookingById([FromBody]BookingRequestDto bookingRequest)
        {
            Request.Headers.TryGetValue("clientId", out var clientId);

            if (clientId.IsNullOrEmpty() || clientId.FirstOrDefault().IsNullOrEmpty())
            {
                return BadRequest();
            }

            var booking = await bookingService.GetBookingsByClientId(clientId.FirstOrDefault(), bookingRequest);

            if (booking.IsNullOrEmpty())
            {
                return NoContent();
            }

            return Ok(booking);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingRequestDto bookingRequestDto)
        {
            Request.Headers.TryGetValue("clientId", out var clientId);

            if (clientId.IsNullOrEmpty() || clientId.FirstOrDefault().IsNullOrEmpty() || bookingRequestDto is null)
            {
                return BadRequest();
            }

            var booking = await bookingService.CreateBooking(clientId.FirstOrDefault(), bookingRequestDto);

            if (booking == null)
            {
                return NoContent();
            }

            return Ok(booking);
        }
    }
}
