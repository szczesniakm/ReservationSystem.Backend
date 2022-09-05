using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Application.Models;
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> MakeReservation([FromBody]MakeReservationRequest request)
        {
            await _reservationsService.MakeReservation(request);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
