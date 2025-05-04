using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Diabetes.Core.Entities;

namespace Diabetes.Data.Configurations
{
    public class FoodItemConfiguration : IEntityTypeConfiguration<FoodItem>
    {
        public void Configure(EntityTypeBuilder<FoodItem> builder)
        {
            builder.ToTable("FoodItems");

            

            builder.Property(fi => fi.FoodName).HasMaxLength(100).IsRequired();
            builder.Property(fi => fi.GlycemicIndex).IsRequired();
            builder.Property(fi => fi.GlycemicCategory).HasMaxLength(50).IsRequired();

            // علاقة One-to-Many مع SuggestedFood
            builder.HasMany(fi => fi.SuggestedFoods)
                .WithOne(sf => sf.FoodItem)
                .HasForeignKey(sf => sf.FoodItemID);
        }
    }
}