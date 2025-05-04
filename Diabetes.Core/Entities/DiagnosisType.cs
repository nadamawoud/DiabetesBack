using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Core.Entities
{
    public class DiagnosisType :BaseEntity
    {
            public string TypeName { get; set; }
            // Navigation property
            public ICollection<MedicalHistory> MedicalHistories { get; set; }       
    }
}
