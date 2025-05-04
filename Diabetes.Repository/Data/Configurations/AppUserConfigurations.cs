using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Diabetes.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Diabetes.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            // تكوين الخصائص الأساسية
            builder.Property(u => u.FullName).HasMaxLength(100);
            builder.Property(u => u.Gender).HasMaxLength(10);
            builder.Property(u => u.BirthDate).HasColumnType("date");
            builder.Property(u => u.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

            // تكوين العلاقات مع جميع الجهات الفاعلة
            builder.HasOne(u => u.CasualUser)
                   .WithOne(c => c.AppUser)
                   .HasForeignKey<CasualUser>(c => c.AppUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.Doctor)
                   .WithOne(d => d.AppUser)
                   .HasForeignKey<Doctor>(d => d.AppUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.Clerk)
                   .WithOne(cl => cl.AppUser)
                   .HasForeignKey<Clerk>(cl => cl.AppUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.Admin)
                   .WithOne(a => a.AppUser)
                   .HasForeignKey<Admin>(a => a.AppUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.Manager)
                   .WithOne(m => m.AppUser)
                   .HasForeignKey<Manager>(m => m.AppUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.Organization)
                   .WithOne(o => o.AppUser)
                   .HasForeignKey<Organization>(o => o.AppUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.NormalizedEmail).IsUnique();
            builder.HasIndex(u => u.NormalizedUserName).IsUnique();
        }
    }
}
