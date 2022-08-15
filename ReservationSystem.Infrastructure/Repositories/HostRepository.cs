using Microsoft.EntityFrameworkCore;
using ReservationSystem.Domain.Entities;
using ReservationSystem.Domain.Models;
using ReservationSystem.Domain.Repositories;
using ReservationSystem.Infrastructure.Extensions;

namespace ReservationSystem.Infrastructure.Repositories
{
    public class HostRepository : IHostRepository
    {
        private readonly ReservationSystemContext _context;

        public HostRepository(ReservationSystemContext context)
        {
            _context = context;
        }
    
        public async Task<IEnumerable<AvaliableHost>> GetAvaliableBetween(DateTime startDate, DateTime endDate)
        {
            var reservedHosts = _context.Reservations
                .Include(x => x.Host)
                .GetBetween(startDate, endDate)
                .ActiveOrUpcoming()
                .Select(x => x.Host.Name);

            var avaliableHosts = await _context.Hosts
                .Include(x => x.Reservations.Where(r => r.StartDate > endDate))
                .Where(h => !reservedHosts.Contains(h.Name))
                .Select(s => new AvaliableHost(s.Name, s.Reservations.OrderBy(r => r.StartDate).FirstOrDefault().StartDate))
                .ToListAsync();

            return avaliableHosts;
        }

        public async Task<Host?> Get(string hostName)
            => await _context.Hosts.FindAsync(hostName);
    }
}
