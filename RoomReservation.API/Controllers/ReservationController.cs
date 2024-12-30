using Microsoft.AspNetCore.Mvc;
using RoomReservation.Application.Services;

namespace RoomReservation.API.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class ReservationController(IReservationService reservationService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await reservationService.GetAllReservations();

            if (reservations == null)
            {
                return NoContent();
            }

            return Ok(reservations);
        }
    }
}
