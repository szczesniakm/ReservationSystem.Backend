using Quartz;
using ReservationSystem.Application.Services;

namespace ReservationSystem.Application.Jobs
{
    internal class UpdateAmtHosts : IJob
    {
        private readonly HostsService _hostsService;

        public UpdateAmtHosts(HostsService hostsService)
        {
            _hostsService = hostsService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _hostsService.UpdateHostsStatus();
        }
    }
}
