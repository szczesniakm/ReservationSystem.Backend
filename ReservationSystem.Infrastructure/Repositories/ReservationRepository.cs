using Microsoft.EntityFrameworkCore;
using ReservationSystem.Domain.Entities;
using ReservationSystem.Domain.Repositories;
using ReservationSystem.Infrastructure.Extensions;

namespace ReservationSystem.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ReservationSystemContext _context;

        public ReservationRepository(ReservationSystemContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Reservation reservation)
        {
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task<Reservation?> GetAsync(int id)
            => await _context.Reservations.Include(x => x.OS).Include(x => x.Host).FirstOrDefaultAsync(x => x.Id == id);

        public async Task UpdateAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsReservedBetween(DateTime start, DateTime end, string hostName) 
            => await _context.Reservations.Include(x => x.Host).GetBetween(start, end).ActiveOrUpcoming().Where(x => x.Host.Name == hostName).AnyAsync();
    }
}
