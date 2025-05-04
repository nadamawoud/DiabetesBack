using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Diabetes.Core.Entities;

namespace Diabetes.Data.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            

            builder.Property(p => p.Name).HasMaxLength(200).IsRequired();
            builder.Property(p => p.Gender).HasMaxLength(10).IsRequired();
            builder.Property(p => p.BirthDate).IsRequired();
            builder.Property(p => p.PhoneNumber).HasMaxLength(20).IsRequired();

            // علاقة Many-to-One مع Clerk
            builder.HasOne(p => p.Clerk)
                .WithMany(c => c.Patients)
                .HasForeignKey(p => p.ClerkID)
                .OnDelete(DeleteBehavior.Restrict);

            // علاقة Many-to-One مع Admin
            builder.HasOne(p => p.Admin)
                .WithMany(a => a.Patients)
                .HasForeignKey(p => p.AdminID)
                .OnDelete(DeleteBehavior.Restrict);

            // علاقة One-to-Many مع MedicalHistory
            builder.HasMany(p => p.MedicalHistories)
                .WithOne(mh => mh.Patient)
                .HasForeignKey(mh => mh.PatientID);

            // علاقة One-to-Many مع SuggestedFood
            builder.HasMany(p => p.SuggestedFoods)
                .WithOne(sf => sf.Patient)
                .HasForeignKey(sf => sf.PatientID);


        }
    }
}