using ReservationSystem.Domain.Entities;

namespace ReservationSystem.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<Reservation> GetReservedBetween(this IQueryable<Reservation> reservations, DateTime start, DateTime end)
            => reservations.Where(r =>
                (r.StartDate >= start && r.StartDate <= end) ||
                (r.EndDate >= start && r.EndDate <= end) ||
                (r.StartDate <= start && r.EndDate >= end));
    }
}
