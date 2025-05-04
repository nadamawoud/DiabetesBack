using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Diabetes.Core.Entities;

namespace Diabetes.Data.Configurations
{
    public class MedicalHistoryConfiguration : IEntityTypeConfiguration<MedicalHistory>
    {
        public void Configure(EntityTypeBuilder<MedicalHistory> builder)
        {
            builder.ToTable("MedicalHistories");

           

            builder.Property(mh => mh.DiagnosisDate).IsRequired();

            // علاقة Many-to-One مع DiagnosisType
            builder.HasOne(mh => mh.DiagnosisType)
                .WithMany(dt => dt.MedicalHistories)
                .HasForeignKey(mh => mh.DiagnosisTypeID);

            // علاقة Many-to-One مع Patient
            builder.HasOne(mh => mh.Patient)
                .WithMany(p => p.MedicalHistories)
                .HasForeignKey(mh => mh.PatientID);

            // علاقة Many-to-One مع Doctor
            builder.HasOne(mh => mh.Doctor)
                .WithMany(d => d.MedicalHistories)
                .HasForeignKey(mh => mh.DoctorID);
        }
    }
}