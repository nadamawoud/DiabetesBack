using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Core.Entities
{
    public class ChatbotAnswerDoctor : BaseEntity
    {
        public string AnswerText { get; set; }
        public DateTime AnswerDate { get; set; }
        // Navigation properties
        public int ChatbotQuestionDoctorID { get; set; } // Foreign Key
        public int DoctorID { get; set; }
        public ChatbotQuestionDoctor ChatbotQuestionDoctor { get; set; }
        public Doctor Doctor { get; set; }
    }
}
