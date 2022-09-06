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

        public async Task UpdateHosts(IEnumerable<Host> hosts)
        {
            var allHostNames = _context.Hosts.ToList();

            _context.Hosts.RemoveRange(allHostNames);
            _context.Hosts.AddRange(hosts);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Host>> GetAll()
            => await _context.Hosts.ToListAsync();
    }
}
