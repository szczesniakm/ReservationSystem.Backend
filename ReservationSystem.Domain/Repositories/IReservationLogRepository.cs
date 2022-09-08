using ReservationSystem.Domain.Entities;

namespace ReservationSystem.Domain.Repositories
{
    public interface IReservationLogRepository
    {
        Task Add(ReservationLog log);
    }
}
