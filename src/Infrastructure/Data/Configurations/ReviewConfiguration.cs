using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {

        builder.HasOne(review => review.Customer)
               .WithMany()
               .HasForeignKey(review => review.CustomerId);
        builder.ToTable("Reviews");
    }
}
