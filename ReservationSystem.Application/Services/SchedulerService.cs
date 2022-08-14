using Quartz;
using ReservationSystem.Application.Jobs;
using ReservationSystem.Domain.Entities;

namespace ReservationSystem.Application.Services
{
    public class SchedulerService
    {
        private readonly ISchedulerFactory _schedulerFactory;

        public SchedulerService(ISchedulerFactory schedulerFactory)
        {
            _schedulerFactory = schedulerFactory ?? throw new ArgumentNullException(nameof(schedulerFactory));
        }

        public async Task SchedulePowerOn(Reservation reservation)
        {
            var scheduler = await _schedulerFactory.GetScheduler();

            var job = JobBuilder
                .Create<PowerOnJob>()
                .WithIdentity(reservation.Id.ToString(), nameof(PowerOnJob))
                .Build();

            var trigger = TriggerBuilder
                .Create()
                .WithIdentity(reservation.Id.ToString(), nameof(PowerOnJob))
                .ForJob(job)
                .StartAt(reservation.StartDate)
                .Build();

            await scheduler.ScheduleJob(job, trigger);

            await scheduler.Start();
        }

        public async Task SchedulePowerOff(Reservation reservation)
        {
            var scheduler = await _schedulerFactory.GetScheduler();

            var job = JobBuilder
                .Create<PowerOffJob>()
                .WithIdentity(reservation.Id.ToString(), nameof(PowerOffJob))
                .Build();

            var trigger = TriggerBuilder
                .Create()
                .WithIdentity(reservation.Id.ToString(), nameof(PowerOffJob))
                .ForJob(job)
                .StartAt(reservation.EndDate)
                .Build();

            await scheduler.ScheduleJob(job, trigger);

            await scheduler.Start();
        }
    }
}
