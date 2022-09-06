using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Application.Models;
using ReservationSystem.Application.Services;

namespace ReservationSystem.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HostsController : ControllerBase
    {
        private readonly HostsService _hostsService;
        public HostsController(HostsService hostsService)
        {
            _hostsService = hostsService;
        }

        [HttpGet("hosts")]
        public async Task<GetAllHostsResponse> GetAvaliableHosts()
            => await _hostsService.GetHosts();
    }
}
