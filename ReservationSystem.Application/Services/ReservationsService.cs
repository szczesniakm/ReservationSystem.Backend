using FluentValidation;
using ReservationSystem.Application.Models;
using ReservationSystem.Domain.Entities;
using ReservationSystem.Domain.Repositories;
using ReservationSystem.Infrastructure.Repositories;

namespace ReservationSystem.Application.Services
{
    public class ReservationsService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IHostRepository _hostRepository;
        private readonly IOSRepository _oSRepository;
        private readonly SchedulerService _schedulerService;
        private readonly IValidator<MakeReservationRequest> _validator;

        public ReservationsService(
            IReservationRepository reservationRepository, 
            IValidator<MakeReservationRequest> validator,
            IHostRepository hostRepository,
            IOSRepository osRepository,
            SchedulerService schedulerService)
        {
            _reservationRepository = reservationRepository;
            _hostRepository = hostRepository;
            _oSRepository = osRepository;
            _validator = validator;
            _schedulerService = schedulerService;
        }

        public async Task MakeReservation(MakeReservationRequest request)
        {
            _validator.ValidateAndThrow(request);

            var host = await _hostRepository.Get(request.HostName);
            if (host is null)
                throw new Exception($"Nie znaleziono hosta {request.HostName}");

            var os = await _oSRepository.Get(request.OsName);
            if (os is null)
                throw new Exception($"Nie znaleziono systemu {request.OsName}");

            var isReserved = await _reservationRepository.IsReservedBetween(request.From, request.To, request.HostName);
            if (isReserved)
                throw new Exception($"Host {request.HostName} jest w tym czasie zarezerwowany.");

            var reservation = new Reservation(host, "admin", os, request.From < DateTime.UtcNow ? DateTime.UtcNow : request.From, request.To);

            await _reservationRepository.CreateAsync(reservation);

            await _schedulerService.SchedulePowerOn(reservation);
            await _schedulerService.SchedulePowerOff(reservation);
        }
    }
}
