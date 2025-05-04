using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Diabetes.Core.Entities;

namespace Diabetes.Data.Configurations
{
    public class CasualUserConfiguration : IEntityTypeConfiguration<CasualUser>
    {
        public void Configure(EntityTypeBuilder<CasualUser> builder)
        {
            builder.ToTable("CasualUsers");

            // العلاقة مع Admin (اختياري)
            builder.HasOne(cu => cu.Admin)
                   .WithMany(a => a.CasualUsers)
                   .HasForeignKey(cu => cu.AdminID)
                   .OnDelete(DeleteBehavior.SetNull);
                   


            // علاقات One-to-Many مع BloodSugars و Alarms و Chatbot
            builder.HasMany(cu => cu.BloodSugars)
                   .WithOne(bs => bs.CasualUser)
                   .HasForeignKey(bs => bs.CasualUserID);

            builder.HasMany(cu => cu.Alarms)
                   .WithOne(a => a.CasualUser)
                   .HasForeignKey(a => a.CasualUserID);

            builder.HasMany(cu => cu.ChatbotResultCasualUsers)
                   .WithOne(crcu => crcu.CasualUser)
                   .HasForeignKey(crcu => crcu.CasualUserID);

            builder.HasMany(cu => cu.ChatbotAnswerCasualUsers)
                   .WithOne(cacu => cacu.CasualUser)
                   .HasForeignKey(cacu => cacu.CasualUserID);
        }
    }
}


