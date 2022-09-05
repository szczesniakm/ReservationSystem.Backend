using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Application.Services;
using ReservationSystem.Infrastructure;

namespace ReservationSystem.Api.Controllers
{
    [Route("api/[controller]")]
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
        {
            var result = await CommandExecutionHelper.ExecuteAsync("pwsh", "-Command ls");
            Console.WriteLine(result);
            return await _ossService.GetDictionary();
        }
    }
}
