namespace ReservationSystem.Domain.Entities
{
    public class Host
    {
        public string Name { get; private set; }
        public string Status { get; private set; }

        private Host() { }

        public Host(string name, string status)
        {
            Name = name;
            Status = status;
        }

        public void SetStatus(string newStatus)
        {
            Status = newStatus;
        }
    }
}