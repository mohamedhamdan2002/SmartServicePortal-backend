using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartGallery.Core.Entities;

namespace SmartGallery.Repository.Data.Configurations
{
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
}
