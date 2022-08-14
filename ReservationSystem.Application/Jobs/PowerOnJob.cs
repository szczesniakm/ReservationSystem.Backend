using Quartz;
using ReservationSystem.Application.Processes;
using ReservationSystem.Domain.Entities;
using ReservationSystem.Domain.Repositories;

namespace ReservationSystem.Application.Jobs
{
    public class PowerOnJob : IJob
    {
        private readonly IReservationRepository _reservationRepository;

        public PowerOnJob(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var reservationId = context.JobDetail.Key.Name;
            var reservation = await _reservationRepository.GetAsync(int.Parse(reservationId));

            var exitCode = await new SetOSProcess(reservation.OS.Name).RunAndReturnExitCode();

            if (exitCode != 0)
                throw new Exception("Wystąpił błąd podczas ustawiania systemu.");

            exitCode = await new PowerOnProcess(reservation.Host.Name).RunAndReturnExitCode();

            if (exitCode != 0)
                throw new Exception("Wystąpił błąd podczas uruchamiania stacji.");

            reservation.SetStatus(ReservationStatus.Active);
            await _reservationRepository.UpdateAsync(reservation);
        }
    }
}
