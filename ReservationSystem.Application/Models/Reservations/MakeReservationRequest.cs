namespace ReservationSystem.Application.Models.Reservations
{
    public record MakeReservationRequest(string HostName, string OsName, DateTime From, DateTime To);
}
