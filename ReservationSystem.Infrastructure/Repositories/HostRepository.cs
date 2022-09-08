using Microsoft.EntityFrameworkCore;
using ReservationSystem.Domain.Entities;
using ReservationSystem.Domain.Repositories;

namespace ReservationSystem.Infrastructure.Repositories
{
    public class HostRepository : IHostRepository
    {
        private readonly ReservationSystemContext _context;

        public HostRepository(ReservationSystemContext context)
        {
            _context = context;
        }

        public async Task<Host?> Get(string hostName)
            => await _context.Hosts.FindAsync(hostName);

        public async Task UpdateHost(Host host)
        {
            _context.Hosts.Update(host);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateHosts(IEnumerable<Host> hosts)
        {
            _context.Hosts.UpdateRange(hosts);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Host>> GetAll()
            => await _context.Hosts.ToListAsync();
    }
}
