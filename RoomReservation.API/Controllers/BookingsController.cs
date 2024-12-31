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
        public async Task<IActionResult> GetBookingById(BookingRequestDto bookingRequest)
        {
            Request.Headers.TryGetValue("clientId", out var clientId);

            if (clientId.IsNullOrEmpty() || clientId.FirstOrDefault().IsNullOrEmpty())
            {
                return BadRequest();
            }
            
            var booking = await bookingService.GetBookingByClientId(clientId.FirstOrDefault(), bookingRequest);

            if (booking == null)
            {
                return NoContent();
            }

            return Ok(booking);
        }
    }
}
