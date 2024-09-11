using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.Property(service => service.Cost)
               .HasColumnType("decimal(18,2)");

        builder.HasOne(service => service.Category)
               .WithMany()
               .HasForeignKey(service => service.CategoryId);

        builder.HasMany(service => service.Reviews)
            .WithOne()
            .HasForeignKey(review => review.ServiceId);
    }
}
