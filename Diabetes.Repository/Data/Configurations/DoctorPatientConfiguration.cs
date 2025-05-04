using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Diabetes.Core.Entities;

public class DoctorPatientConfiguration : IEntityTypeConfiguration<DoctorPatient>
{
    public void Configure(EntityTypeBuilder<DoctorPatient> builder)
    {
        builder.ToTable("DoctorPatients");
        builder.HasKey(dp => new { dp.DoctorID, dp.PatientID });

        builder.HasOne(dp => dp.Doctor)
            .WithMany(d => d.DoctorPatients)
            .HasForeignKey(dp => dp.DoctorID)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(dp => dp.Patient)
            .WithMany(p => p.DoctorPatients)
            .HasForeignKey(dp => dp.PatientID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}