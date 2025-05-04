using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Core.Entities
{
    public class Doctor 
    {
        public int ID { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string DoctorSpecialization { get; set; }
        public string MedicalSyndicateCardNumber { get; set; }
        public bool IsApproved { get; set; } = false;
        // Navigation properties
        public int? AdminID { get; set; }
        public Admin? Admin { get; set; }
        public DoctorApproval DoctorApproval { get; set; }
        //many to many relation
        public ICollection<MedicalHistory> MedicalHistories { get; set; }
        public ICollection<SuggestedFood> SuggestedFoods { get; set; }
        public ICollection<ChatbotAnswerDoctor> ChatbotAnswerDoctors { get; set; }
        public ICollection<Patient> Patients { get; set; }
        public ICollection<DoctorPatient> DoctorPatients { get; set; }
    }
}
