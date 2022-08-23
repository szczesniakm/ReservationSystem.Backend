namespace ReservationSystem.Infrastructure.Settings
{
    public class LdapSettings
    {
        public string ServerAddress { get; set; }
        public int ProtocolVersion { get; set; }
        public string LoginDN { get; set; }
        public string GroupsSearchBase { get; set; }
        public string GroupsSearchFilter { get; set; }
    }
}
