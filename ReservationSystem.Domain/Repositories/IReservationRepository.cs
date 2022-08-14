using ReservationSystem.Domain.Entities;

namespace ReservationSystem.Domain.Repositories
{
    public interface IReservationRepository
    {
        Task CreateAsync(Reservation reservation);
        Task DeleteAsync(Reservation reservation);
        Task<Reservation?> GetAsync(int id);
        Task UpdateAsync(Reservation reservation);
        Task<bool> IsReservedBetween(DateTime start, DateTime end, string hostName);
    }
}
