using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Diabetes.Core.Entities;

namespace Diabetes.Data.Configurations
{
    public class AlarmConfiguration : IEntityTypeConfiguration<Alarm>
    {
        public void Configure(EntityTypeBuilder<Alarm> builder)
        {
            builder.ToTable("Alarms");

            

            builder.Property(a => a.AlarmType).HasMaxLength(100).IsRequired();
            builder.Property(a => a.AlarmTime).IsRequired();

            // علاقة Many-to-One مع CasualUser
            builder.HasOne(a => a.CasualUser)
                .WithMany(cu => cu.Alarms)
                .HasForeignKey(a => a.CasualUserID);
        }
    }
}