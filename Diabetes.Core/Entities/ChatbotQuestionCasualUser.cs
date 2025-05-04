using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Core.Entities
{
    public class ChatbotQuestionCasualUser : BaseEntity
    {          
            public string QuestionText { get; set; }

        // Navigation properties
        public int? AdminID { get; set; }
        public Admin? Admin { get; set; }        
        public ChatbotAnswerCasualUser ChatbotAnswerCasualUser { get; set; }
    }
}