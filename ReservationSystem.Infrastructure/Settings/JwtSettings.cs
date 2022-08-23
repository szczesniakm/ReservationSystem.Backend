namespace ReservationSystem.Infrastructure.Settings
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Subject { get; set; }
        public string Secret { get; set; }
        public int ExpiryMinutes { get; set; }
    }
}
