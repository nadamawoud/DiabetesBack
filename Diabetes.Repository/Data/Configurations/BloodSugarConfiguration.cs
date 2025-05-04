using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Diabetes.Core.Entities;

namespace Diabetes.Data.Configurations
{
    public class BloodSugarConfiguration : IEntityTypeConfiguration<BloodSugar>
    {
        public void Configure(EntityTypeBuilder<BloodSugar> builder)
        {
            builder.ToTable("BloodSugars");

            

            builder.Property(bs => bs.Period).HasMaxLength(50).IsRequired();
            builder.Property(bs => bs.GlucoseLevel).IsRequired();
            builder.Property(bs => bs.MeasurementDate).IsRequired();

            // علاقة Many-to-One مع CasualUser
            builder.HasOne(bs => bs.CasualUser)
                .WithMany(cu => cu.BloodSugars)
                .HasForeignKey(bs => bs.CasualUserID);
        }
    }
}