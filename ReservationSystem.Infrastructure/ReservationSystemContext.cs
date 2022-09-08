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
            optionsBuilder
                .UseSqlite(_databaseSettings.DatabaseFile)
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HostEntityTypeConfiguration).Assembly);

            modelBuilder.Entity<Host>().HasData(
                new Host("s1", ""),
                new Host("s2", ""),
                new Host("s3", ""),
                new Host("s4", ""),
                new Host("s5", ""),
                new Host("s6", ""),
                new Host("s7", ""),
                new Host("s8", ""),
                new Host("s9", "")
                );

            modelBuilder.Entity<OS>().HasData(
                new OS("archlinux console"),
                new OS("windows 10")
                );
        }
    }
}
