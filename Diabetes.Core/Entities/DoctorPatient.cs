using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Core.Entities
{   
        public class DoctorPatient
        {
            // Foreign Key for Doctor
            public int DoctorID { get; set; }
            // Navigation property to Doctor
            public Doctor Doctor { get; set; }
            // Foreign Key for Patient
            public int PatientID { get; set; }
            // Navigation property to Patient
            public Patient Patient { get; set; }
            // يمكن إضافة خصائص إضافية للعلاقة إذا لزم الأمر
            // مثل تاريخ إنشاء العلاقة أو حالة التحويل
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
            public string ReferralStatus { get; set; } // مثال: "Pending", "Accepted", "Rejected"
        }
}
