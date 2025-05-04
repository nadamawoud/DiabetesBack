using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Diabetes.Core.Entities;

namespace Diabetes.Data.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctors");

            builder.Property(d => d.DoctorSpecialization).HasMaxLength(100).IsRequired();

            builder.HasOne(d => d.Admin)
                   .WithMany(a => a.Doctors)
                   .HasForeignKey(d => d.AdminID)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);

            
            builder.HasOne(d => d.DoctorApproval)
                   .WithOne(da => da.Doctor)
                   .HasForeignKey<DoctorApproval>(da => da.DoctorID);

            builder.HasMany(d => d.MedicalHistories)
                   .WithOne(mh => mh.Doctor)
                   .HasForeignKey(mh => mh.DoctorID);

            builder.HasMany(d => d.SuggestedFoods)
                   .WithOne(sf => sf.Doctor)
                   .HasForeignKey(sf => sf.DoctorID);

            builder.HasMany(d => d.ChatbotAnswerDoctors)
                   .WithOne(cad => cad.Doctor)
                   .HasForeignKey(cad => cad.DoctorID);
        }
    }
}