namespace ReservationSystem.Domain.Entities
{
    public class OS
    {
        public string Name { get; set; }

        private OS() { }

        public OS(string name)
        {
            Name = name;
        }
    }
}