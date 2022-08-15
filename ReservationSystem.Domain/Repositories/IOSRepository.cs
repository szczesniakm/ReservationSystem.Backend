using ReservationSystem.Domain.Entities;

namespace ReservationSystem.Domain.Repositories
{
    public interface IOSRepository
    {
        Task<OS?> Get(string osName);
        Task<IEnumerable<string>> GetDictionary();
    }
}
