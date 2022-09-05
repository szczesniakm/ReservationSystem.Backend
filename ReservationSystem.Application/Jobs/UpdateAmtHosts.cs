using Quartz;
using ReservationSystem.Domain.Repositories;
using ReservationSystem.Infrastructure;

namespace ReservationSystem.Application.Jobs
{
    internal class UpdateAmtHosts : IJob
    {
        private readonly IReservationRepository _reservationRepository;

        public UpdateAmtHosts(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var result = await CommandExecutionHelper.ExecuteAsync("./meshcmd", "amtscan --scan 10.146.255.0/24");
            Console.WriteLine(result);
        }
    }
}
