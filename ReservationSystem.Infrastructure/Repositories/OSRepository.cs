using Microsoft.EntityFrameworkCore;
using ReservationSystem.Domain.Entities;
using ReservationSystem.Domain.Repositories;

namespace ReservationSystem.Infrastructure.Repositories
{
    public class OSRepository : IOSRepository
    {
        private readonly ReservationSystemContext _context;

        public OSRepository(ReservationSystemContext context)
        {
            _context = context;
        }

        public async Task<OS?> Get(string osName)
            => await _context.OSs.FindAsync(osName);

        public async Task<IEnumerable<string>> GetDictionary()
            => await _context.OSs.Select(x => x.Name).ToListAsync();
    }
}
