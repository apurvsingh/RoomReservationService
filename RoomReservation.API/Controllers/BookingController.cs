using Microsoft.AspNetCore.Mvc;
using RoomReservation.Application.Services;

namespace RoomReservation.API.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingController(IBookingService bookingService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            var reservations = await bookingService.GetBookings();

            if (reservations == null)
            {
                return NoContent();
            }

            return Ok(reservations);
        }
    }
}
