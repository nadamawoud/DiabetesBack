using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Core.Entities
{
   public class ChatbotAnswerCasualUser : BaseEntity
    {
            public string AnswerText { get; set; }
            public DateTime AnswerDate { get; set; }

        // Navigation properties
        public int ChatbotQuestionCasualUserID { get; set; }
        public int CasualUserID { get; set; }
        public ChatbotQuestionCasualUser ChatbotQuestionCasualUser { get; set; }
        public CasualUser CasualUser { get; set; }
        
    }
}
