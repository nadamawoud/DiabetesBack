using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Diabetes.Core.Entities;

namespace Diabetes.Data.Configurations
{
    public class SuggestedFoodConfiguration : IEntityTypeConfiguration<SuggestedFood>
    {
        public void Configure(EntityTypeBuilder<SuggestedFood> builder)
        {
            builder.ToTable("SuggestedFoods");

            

            builder.Property(sf => sf.SuggestedDate).IsRequired();

            // علاقة Many-to-One مع Patient
            builder.HasOne(sf => sf.Patient)
                .WithMany(p => p.SuggestedFoods)
                .HasForeignKey(sf => sf.PatientID)
                  .OnDelete(DeleteBehavior.Restrict);

            // علاقة Many-to-One مع FoodItem
            builder.HasOne(sf => sf.FoodItem)
                .WithMany(fi => fi.SuggestedFoods)
                .HasForeignKey(sf => sf.FoodItemID)
                  .OnDelete(DeleteBehavior.Restrict);

            // علاقة Many-to-One مع Doctor
            builder.HasOne(sf => sf.Doctor)
                .WithMany(d => d.SuggestedFoods)
                .HasForeignKey(sf => sf.DoctorID)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}