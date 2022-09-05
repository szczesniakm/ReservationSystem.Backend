using Quartz;
using ReservationSystem.Domain.Entities;
using ReservationSystem.Domain.Repositories;
using ReservationSystem.Infrastructure;
using System.Text.RegularExpressions;

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
            var result = await CommandExecutionHelper.ExecuteAsync("./meshcmd", "amtscan --scan 10.146.225.0/24");
            Regex IPAd = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
            MatchCollection MatchResult = IPAd.Matches(result);
            var hosts = MatchResult.ToList().Select(x => new Host(x.ToString())).ToList();
            hosts.ForEach(async x =>
            {
                var status = await CommandExecutionHelper.ExecuteAsync("./meshcmd", $"sudo ./meshcmd AmtPower --host {x.Name} --pass");
                Console.WriteLine(status);
            });
        }
    }
}
