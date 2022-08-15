using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReservationSystem.Domain.Entities;

namespace ReservationSystem.Infrastructure.EntityConfiguration
{
    public class ReservationEntityTypeConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder
                .HasOne(x => x.Host)
                .WithMany(x => x.Reservations)
                .IsRequired();

            builder
                .HasOne(x => x.OS)
                .WithMany()
                .IsRequired();

            builder
                .Property(x => x.StartDate)
                .HasConversion<DateTimeToTicksConverter>()
                .IsRequired();

            builder
                .Property(x => x.EndDate)
                .HasConversion<DateTimeToTicksConverter>()
                .IsRequired();

            builder
                .Property(x => x.Username)
                .IsRequired();
        }
    }
}
