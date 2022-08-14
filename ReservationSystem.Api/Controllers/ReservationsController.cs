using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Application.Models.Reservations;
using ReservationSystem.Application.Services;

namespace ReservationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationsService _reservationsService;
        public ReservationsController(ReservationsService reservationService)
        {
            _reservationsService = reservationService;
        }

        [HttpPost]
        public async Task<IActionResult> MakeReservation([FromBody]MakeReservationRequest request)
        {
            await _reservationsService.MakeReservation(request);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
