using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartGallery.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGallery.Repository.Data.Configurations
{
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
}
