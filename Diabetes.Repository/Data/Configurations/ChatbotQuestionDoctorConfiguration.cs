using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Diabetes.Core.Entities;

namespace Diabetes.Data.Configurations
{
    public class ChatbotQuestionDoctorConfiguration : IEntityTypeConfiguration<ChatbotQuestionDoctor>
    {
        public void Configure(EntityTypeBuilder<ChatbotQuestionDoctor> builder)
        {
            builder.ToTable("ChatbotQuestionDoctors");

            

            builder.Property(cqd => cqd.QuestionText).IsRequired();

            // علاقة Many-to-One مع Admin
            builder.HasOne(cqd => cqd.Admin)
                .WithMany(a => a.ChatbotQuestionDoctors)
                .HasForeignKey(cqd => cqd.AdminID)
                .OnDelete(DeleteBehavior.SetNull);

            // علاقة One-to-One مع ChatbotAnswerDoctor
            builder.HasOne(cqd => cqd.ChatbotAnswerDoctor)
                .WithOne(cad => cad.ChatbotQuestionDoctor)
                .HasForeignKey<ChatbotAnswerDoctor>(cad => cad.ChatbotQuestionDoctorID);
        }
    }
}