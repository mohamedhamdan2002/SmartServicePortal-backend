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
    internal class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {

            builder.Property(reservation => reservation.Status)
                .HasMaxLength(10)
                .HasConversion(
                        status => status.ToString(), // store in database as string
                        status => (StatusEnum)Enum.Parse(typeof(StatusEnum), status) // convert from string to enum 
                   );

            builder.HasOne(reservation => reservation.Service)
                .WithMany()
                .HasForeignKey(reservation => reservation.ServiceId);

            builder.HasOne(reservation => reservation.Customer)
                .WithMany()
                .HasForeignKey(reservation => reservation.CustomerId);
            
            builder.OwnsOne(reservation => reservation.Address);
            builder.OwnsOne(reservation => reservation.Contact);
        }
    }
}
