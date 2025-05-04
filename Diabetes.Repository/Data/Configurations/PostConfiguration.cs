using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Diabetes.Core.Entities;

namespace Diabetes.Data.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts");


            builder.Property(p => p.Title).HasMaxLength(200).IsRequired();
            builder.Property(p => p.Content).IsRequired();
            builder.Property(p => p.ImageURL).HasMaxLength(500);
            builder.Property(p => p.VideoURL).HasMaxLength(500);
            builder.Property(p => p.PublishDate).IsRequired();

            // علاقة Many-to-One مع Organization
            builder.HasOne(p => p.Organization)
                .WithMany(o => o.Posts)
                .HasForeignKey(p => p.OrganizationID)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}