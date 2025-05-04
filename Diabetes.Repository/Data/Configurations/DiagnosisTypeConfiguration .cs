using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Diabetes.Core.Entities;

namespace Diabetes.Data.Configurations
{
    public class DiagnosisTypeConfiguration : IEntityTypeConfiguration<DiagnosisType>
    {
        public void Configure(EntityTypeBuilder<DiagnosisType> builder)
        {
            builder.ToTable("DiagnosisTypes");

            

            builder.Property(dt => dt.TypeName).HasMaxLength(100).IsRequired();

            // علاقة One-to-Many مع MedicalHistory
            builder.HasMany(dt => dt.MedicalHistories)
                .WithOne(mh => mh.DiagnosisType)
                .HasForeignKey(mh => mh.DiagnosisTypeID);
        }
    }
}