using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Application.Models;
using ReservationSystem.Application.Services;
using ReservationSystem.Domain.Models;

namespace ReservationSystem.Api.Controllers
{
    [Authorize(Roles = @"cn=admin,ou=groups,dc=example,dc=com")]
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
