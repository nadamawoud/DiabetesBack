using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Core.Entities
{
    public class Patient : BaseEntity
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? CreatedAt { get; set; }
        // Navigation properties
        public int ClerkID { get; set; }
        public int AdminID { get; set; }
        public Clerk Clerk { get; set; }
        public Admin Admin { get; set; }
        //Many to 1
        public ICollection<MedicalHistory> MedicalHistories { get; set; }
        public ICollection<SuggestedFood> SuggestedFoods { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
        public  ICollection<DoctorPatient> DoctorPatients { get; set; } 
    }

}

