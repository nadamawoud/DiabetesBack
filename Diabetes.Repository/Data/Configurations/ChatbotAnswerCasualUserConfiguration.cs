using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Diabetes.Core.Entities;

namespace Diabetes.Data.Configurations
{
    public class ChatbotAnswerCasualUserConfiguration : IEntityTypeConfiguration<ChatbotAnswerCasualUser>
    {
        public void Configure(EntityTypeBuilder<ChatbotAnswerCasualUser> builder)
        {
            builder.ToTable("ChatbotAnswerCasualUsers");

           

            builder.Property(cacu => cacu.AnswerText).IsRequired();
            builder.Property(cacu => cacu.AnswerDate).IsRequired();

            // علاقة One-to-One مع ChatbotQuestionCasualUser
            builder.HasOne(cacu => cacu.ChatbotQuestionCasualUser)
                .WithOne(cqcu => cqcu.ChatbotAnswerCasualUser)
                .HasForeignKey<ChatbotAnswerCasualUser>(cacu => cacu.ChatbotQuestionCasualUserID)
                .OnDelete(DeleteBehavior.NoAction);

            // علاقة Many-to-One مع CasualUser
            builder.HasOne(cacu => cacu.CasualUser)
                .WithMany(cu => cu.ChatbotAnswerCasualUsers)
                .HasForeignKey(cacu => cacu.CasualUserID)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}