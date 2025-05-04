using System;

namespace Diabetes.Core.Entities
{
    public class ChatbotQuestionDoctor : BaseEntity
    {
        public string QuestionText { get; set; }
        // Navigation properties
        public int? AdminID { get; set; }
        public Admin? Admin { get; set; }     
        public ChatbotAnswerDoctor ChatbotAnswerDoctor { get; set; } // One-to-One
    }
}