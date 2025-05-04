using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Diabetes.Core.Entities;

namespace Diabetes.Data.Configurations
{
    public class ChatbotResultCasualUserConfiguration : IEntityTypeConfiguration<ChatbotResultCasualUser>
    {
        public void Configure(EntityTypeBuilder<ChatbotResultCasualUser> builder)
        {
            builder.ToTable("ChatbotResultCasualUsers");

       
            // تكوين الخصائص
            builder.Property(c => c.Result)
                .IsRequired()
                .HasMaxLength(1000); // يمكن تعديل الطول حسب الحاجة

            builder.Property(c => c.ResultDate)
                .IsRequired()
                .HasColumnType("datetime");

            // علاقة Many-to-One مع CasualUser
            builder.HasOne(c => c.CasualUser)
                .WithMany(u => u.ChatbotResultCasualUsers) // تأكد من وجود هذه الخاصية في كيان CasualUser
                .HasForeignKey(c => c.CasualUserID)
                .OnDelete(DeleteBehavior.Cascade); // أو Restrict حسب احتياجاتك

            // إضافة فهارس إذا لزم الأمر
            builder.HasIndex(c => c.CasualUserID);
            builder.HasIndex(c => c.ResultDate);
        }
    }
}