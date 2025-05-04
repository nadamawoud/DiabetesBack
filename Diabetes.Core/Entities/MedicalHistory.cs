using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Core.Entities
{
    public class MedicalHistory : BaseEntity
    {
        // Navigation properties     
        public DateTime DiagnosisDate { get; set; }
        public int DiagnosisTypeID { get; set; }
        public DiagnosisType DiagnosisType { get; set; }
        public int PatientID { get; set; }                 
         public Patient Patient { get; set; }
         public int DoctorID { get; set; }
         public Doctor Doctor { get; set; }       
    }
}
