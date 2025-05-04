using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Diabetes.Core.Entities;

namespace Diabetes.Data.Configurations
{
    public class DoctorApprovalConfiguration : IEntityTypeConfiguration<DoctorApproval>
    {
        public void Configure(EntityTypeBuilder<DoctorApproval> builder)
        {
            builder.ToTable("DoctorApprovals");

           

            builder.Property(da => da.ApprovalStatus).HasMaxLength(50).IsRequired();
            builder.Property(da => da.ApprovalDate).IsRequired(false);

            builder.HasOne(da => da.Doctor)        // DoctorApproval له Doctor واحد
             .WithOne(d => d.DoctorApproval) // Doctor له DoctorApproval واحد
              .HasForeignKey<DoctorApproval>(da => da.DoctorID); // ForeignKey في جدول DoctorApproval



            // علاقة Many-to-One مع Organization
            builder.HasOne(da => da.Organization)
                .WithMany(o => o.DoctorApprovals)
                .HasForeignKey(da => da.OrganizationID);
        }
    }
}
