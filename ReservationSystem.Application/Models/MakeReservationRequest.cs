namespace ReservationSystem.Application.Models
{
    public record MakeReservationRequest(string HostName, string OsName, DateTime From, DateTime To);
}
