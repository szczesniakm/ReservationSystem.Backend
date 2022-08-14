using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationSystem.Domain.Entities;

namespace ReservationSystem.Infrastructure.EntityConfiguration
{
    public class OSEntityTypeConfiguration : IEntityTypeConfiguration<OS>
    {
        public void Configure(EntityTypeBuilder<OS> builder)
        {
            builder
                .HasKey(x => x.Name);
        }
    }
}
