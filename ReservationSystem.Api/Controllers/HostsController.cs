using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Application.Models;
using ReservationSystem.Application.Services;

namespace ReservationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class HostsController : ControllerBase
    {
        private readonly HostsService _hostsService;
        public HostsController(HostsService hostsService)
        {
            _hostsService = hostsService;
        }

        [HttpGet]
        public async Task<GetAllHostsResponse> GetAvaliableHosts()
            => await _hostsService.GetHosts();

        [HttpPut("{hostName}")]
        public async Task<IActionResult> PowerOnHost(string hostName, [FromBody]PowerOnRequest body)
        {
            var username = User.Identity?.Name;

            var request = body with
            {
                HostName = hostName,
                Username = username!
            };

            await _hostsService.PowerOn(request);
            return NoContent();
        }
    }
}
