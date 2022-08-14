namespace ReservationSystem.Domain.Entities
{
    public class Host
    {
        public string Name { get; private set; }
        public ICollection<Reservation>? Reservations { get; private set; }

        private Host() { }

        public Host(string name)
        {
            Name = name;
        }
    }
}