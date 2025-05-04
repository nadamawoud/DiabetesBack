using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Core.Entities
{
    public class Clerk 
    {
        public int ID { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string LicenseCode { get; set; }
        public string? VerificationCode { get; set; }
        public bool IsEmailVerified { get; set; } = false;
        public int? AdminID { get; set; }
        // Navigation Property
        public Admin? Admin { get; set; } // العلاقة مع Admin
        //Relation many
        public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
    }
}
