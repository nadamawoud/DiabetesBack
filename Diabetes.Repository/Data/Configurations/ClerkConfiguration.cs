using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Diabetes.Core.Entities;

namespace Diabetes.Data.Configurations
{
    public class ClerkConfiguration : IEntityTypeConfiguration<Clerk>
    {
        public void Configure(EntityTypeBuilder<Clerk> builder)
        {
            builder.ToTable("Clerks");

            builder.HasOne(c => c.Admin)
                   .WithMany(a => a.Clerks)
                   .HasForeignKey(c => c.AdminID)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);

            
            builder.HasMany(c => c.Patients)
                   .WithOne(p => p.Clerk)
                   .HasForeignKey(p => p.ClerkID);
        }
    }
}
