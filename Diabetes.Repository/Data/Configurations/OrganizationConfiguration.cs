using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Diabetes.Core.Entities;

namespace Diabetes.Data.Configurations
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.ToTable("Organizations");

            builder.Property(o => o.IsMedicalSyndicate).IsRequired();

            builder.HasOne(o => o.Admin)
                   .WithMany(a => a.Organizations)
                   .HasForeignKey(o => o.AdminID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(o => o.Posts)
                   .WithOne(p => p.Organization)
                   .HasForeignKey(p => p.OrganizationID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(o => o.DoctorApprovals)
                   .WithOne(da => da.Organization)
                   .HasForeignKey(da => da.OrganizationID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}