using ReservationSystem.Domain.Entities;

namespace ReservationSystem.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<Reservation> GetBetween(this IQueryable<Reservation> reservations, DateTime start, DateTime end)
            => reservations.Where(r =>
                (r.StartDate >= start && r.StartDate <= end) ||
                (r.EndDate >= start && r.EndDate <= end) ||
                (r.StartDate <= start && r.EndDate >= end));

        public static IQueryable<Reservation> ActiveOrUpcoming(this IQueryable<Reservation> reservations)
            => reservations.Where(r => r.Status == ReservationStatus.Active || r.Status == ReservationStatus.Upcoming);
    }
}
