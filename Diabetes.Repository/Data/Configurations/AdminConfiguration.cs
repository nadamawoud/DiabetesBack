using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Diabetes.Core.Entities;

namespace Diabetes.Data.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.Property(a => a.AppUserId).IsRequired();
            // العلاقة مع AppUser (واحد لواحد)
            builder.HasOne(a => a.AppUser)
                   .WithOne(u => u.Admin)
                   .HasForeignKey<Admin>(a => a.AppUserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // العلاقة مع Manager (واحد لواحد)
            builder.HasOne(a => a.Manager)
                   .WithOne(m => m.Admin)
                   .HasForeignKey<Admin>(a => a.ManagerID)
                   .OnDelete(DeleteBehavior.SetNull); // يمكن تعديل الحذف إذا لزم الأمر

            // علاقات One-to-Many مع Clerks و Doctors و Patients و Organizations و CasualUsers
            builder.HasMany(a => a.Clerks)
                   .WithOne(c => c.Admin)
                   .HasForeignKey(c => c.AdminID)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(a => a.Doctors)
                   .WithOne(d => d.Admin)
                   .HasForeignKey(d => d.AdminID)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(a => a.Patients)
                   .WithOne(p => p.Admin)
                   .HasForeignKey(p => p.AdminID)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(a => a.Organizations)
                   .WithOne(o => o.Admin)
                   .HasForeignKey(o => o.AdminID)
                   .OnDelete(DeleteBehavior.Restrict);  // Restrict for organizations

            builder.HasMany(a => a.CasualUsers)
                   .WithOne(c => c.Admin)
                   .HasForeignKey(c => c.AdminID)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(a => a.ChatbotQuestionDoctors)
                   .WithOne(c => c.Admin)
                   .HasForeignKey(c => c.AdminID)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(a => a.ChatbotQuestionCasualUsers)
                   .WithOne(c => c.Admin)
                   .HasForeignKey(c => c.AdminID)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
