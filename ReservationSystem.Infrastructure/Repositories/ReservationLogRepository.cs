using ReservationSystem.Domain.Entities;
using ReservationSystem.Domain.Repositories;

namespace ReservationSystem.Infrastructure.Repositories
{
    public class ReservationLogRepository : IReservationLogRepository
    {
        private readonly ReservationSystemContext _context;

        public ReservationLogRepository(ReservationSystemContext context)
        {
            _context = context;
        }

        public async Task Add(ReservationLog log)
        {
            await _context.ReseservationLogs.AddAsync(log);
            await _context.SaveChangesAsync();
        }
    }
}
