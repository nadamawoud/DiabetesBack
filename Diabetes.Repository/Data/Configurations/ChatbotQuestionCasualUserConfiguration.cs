using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Diabetes.Core.Entities;

namespace Diabetes.Data.Configurations
{
    public class ChatbotQuestionCasualUserConfiguration : IEntityTypeConfiguration<ChatbotQuestionCasualUser>
    {
        public void Configure(EntityTypeBuilder<ChatbotQuestionCasualUser> builder)
        {
            builder.ToTable("ChatbotQuestionCasualUsers");

            

            builder.Property(cqcu => cqcu.QuestionText).IsRequired();

            // علاقة Many-to-One مع Admin
            builder.HasOne(cqcu => cqcu.Admin)
                .WithMany(a => a.ChatbotQuestionCasualUsers)
                .HasForeignKey(cqcu => cqcu.AdminID)
                .OnDelete(DeleteBehavior.SetNull);

            // علاقة One-to-One مع ChatbotAnswerCasualUser
            builder.HasOne(cqcu => cqcu.ChatbotAnswerCasualUser)
                .WithOne(cacu => cacu.ChatbotQuestionCasualUser)
                .HasForeignKey<ChatbotAnswerCasualUser>(cacu => cacu.ChatbotQuestionCasualUserID);
        }
    }
}