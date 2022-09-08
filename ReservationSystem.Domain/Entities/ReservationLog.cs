namespace ReservationSystem.Domain.Entities
{
    public class ReservationLog
    {
        public int Id { get; private set; }
        public string Host { get; private set; }
        public string Username { get; private set; }
        public string OS { get; private set; }
        public DateTime Date { get; private set; }
        
        private ReservationLog() { }

        private ReservationLog(string host, string username, string operatingSystem, DateTime date)
        {
            Host = host;
            Username = username;
            OS = operatingSystem;
            Date = date;
        }

        public static ReservationLog Create(string host, string username, string operatingSystem)
            => new(host, username, operatingSystem, DateTime.Now);
    }
}
