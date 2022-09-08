using ReservationSystem.Domain.Entities;

namespace ReservationSystem.Domain.Repositories
{
    public interface IHostRepository
    {
        Task<IEnumerable<Host>> GetAll();
        Task<Host?> Get(string hostName);
        Task UpdateHost(Host host);
        Task UpdateHosts(IEnumerable<Host> hosts);
    }
}
