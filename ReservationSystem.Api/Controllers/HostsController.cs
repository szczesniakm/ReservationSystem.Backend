using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Application.Models.Hosts;
using ReservationSystem.Application.Services;
using ReservationSystem.Domain.Models;

namespace ReservationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostsController : ControllerBase
    {
        private readonly HostsService _hostsService;
        public HostsController(HostsService hostsService)
        {
            _hostsService = hostsService;
        }

        [HttpGet("avaliable-hosts")]
        public async Task<IEnumerable<AvaliableHost>> GetAvaliableHosts([FromQuery]GetAvaliableHostsRequest request)
            => await _hostsService.GetAvaliableHosts(request);
    }
}
