using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Application.Services;

namespace ReservationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OSsController : ControllerBase
    {
        private readonly OSsService _ossService;
        public OSsController(OSsService ossService)
        {
            _ossService = ossService;
        }

        [HttpGet("dictionary")]
        public async Task<IEnumerable<string>> GetAvaliableHosts()
            => await _ossService.GetDictionary();
    }
}
