using ReservationSystem.Domain.Entities;
using ReservationSystem.Domain.Models;

namespace ReservationSystem.Domain.Repositories
{
    public interface IHostRepository
    {
        Task<IEnumerable<AvaliableHost>> GetAvaliableBetween(DateTime startDate, DateTime endDate);
        Task<Host?> Get(string hostName);
    }
}
