namespace ReservationSystem.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; private set; }
        public Host Host { get; private set; }
        public string Username { get; private set; }
        public OS OS { get; private set; }
        public ReservationStatus Status { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        
        private Reservation() { }

        public Reservation(Host host, string username, OS operatingSystem, DateTime startDate, DateTime endDate)
        {
            Host = host;
            Username = username;
            OS = operatingSystem;
            Status = ReservationStatus.Upcoming;
            StartDate = startDate;
            EndDate = endDate;
        }

        public void SetStatus(ReservationStatus status)
        {
            Status = status;
        }
    }
}
