using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Core.Entities
{
    public class CasualUser 
    {
        public int ID { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string? VerificationCode { get; set; }
        public bool EmailVerified { get; set; } = false;
        // Navigation properties
        public int? AdminID { get; set; }
        public Admin? Admin { get; set; }
        public ICollection<BloodSugar> BloodSugars { get; set; }
        public ICollection<Alarm> Alarms { get; set; }
        public ICollection<ChatbotResultCasualUser> ChatbotResultCasualUsers { get; set; }
        public ICollection<ChatbotAnswerCasualUser> ChatbotAnswerCasualUsers { get; set; }
    }
}
