namespace ReservationSystem.Application.Models
{
    public record GetAllHostsResponse(IReadOnlyCollection<GetAllHostsResponse.Host> Hosts)
    {
        public record Host(string Name, string Status);
    };
}
