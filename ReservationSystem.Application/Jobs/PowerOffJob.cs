using Quartz;
using ReservationSystem.Application.Processes;
using ReservationSystem.Domain.Entities;
using ReservationSystem.Domain.Repositories;

namespace ReservationSystem.Application.Jobs
{
    public class PowerOffJob : IJob
    {
        private readonly IReservationRepository _reservationRepository;

        public PowerOffJob(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var reservationId = context.JobDetail.Key.Name;
            var reservation = await _reservationRepository.GetAsync(int.Parse(reservationId));

            var exitCode = await new PowerOffProcess(reservation.Host.Name).RunAndReturnExitCode();

            if (exitCode != 0)
                throw new Exception("Wystąpił błąd podczas wyłączania stacji.");

            reservation.SetStatus(ReservationStatus.Ended);
            await _reservationRepository.UpdateAsync(reservation);
        }
    }
}
