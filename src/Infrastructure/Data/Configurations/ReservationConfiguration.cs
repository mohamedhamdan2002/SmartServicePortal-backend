using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

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
        builder.ToTable("Reservations");
    }
}
