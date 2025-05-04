using Diabetes.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Core.Entities
{
    public class Admin 
    {
        public int ID { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int? ManagerID { get; set; }
        public Manager Manager { get; set; }
        public ICollection<Clerk> Clerks { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
        public ICollection<Patient> Patients { get; set; }
        public ICollection<Organization> Organizations { get; set; }
        public ICollection<CasualUser> CasualUsers { get; set; }
        public ICollection<ChatbotQuestionDoctor> ChatbotQuestionDoctors { get; set; }
        public ICollection<ChatbotQuestionCasualUser> ChatbotQuestionCasualUsers { get; set; }
    }
}
