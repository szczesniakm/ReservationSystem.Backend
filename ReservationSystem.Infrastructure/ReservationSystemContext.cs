using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ReservationSystem.Domain.Entities;
using ReservationSystem.Infrastructure.EntityConfiguration;
using ReservationSystem.Infrastructure.Settings;

namespace ReservationSystem.Infrastructure
{
    public class ReservationSystemContext : DbContext
    {
        private readonly DatabaseSettings _databaseSettings;

        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Host> Hosts { get; set; }
        public DbSet<OS> OSs { get; set; }

        public ReservationSystemContext(IOptions<DatabaseSettings> databaseSettings)
        {
            _databaseSettings = databaseSettings.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_databaseSettings.DatabaseFile);
            optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReservationEntityTypeConfiguration).Assembly);
        }
    }
}
