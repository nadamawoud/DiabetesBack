using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Core.Entities
{
    public class DoctorApproval : BaseEntity
    {
            public string ApprovalStatus { get; set; } = "Pending";
            public DateTime? ApprovalDate { get; set; }
        // Navigation properties
        public int DoctorID { get; set; }
        public int OrganizationID { get; set; }
        public Doctor Doctor { get; set; }
        public Organization Organization { get; set; }     
    }
}
