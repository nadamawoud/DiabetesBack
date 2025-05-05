using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Core.DTOs
{
    public class DoctorApprovalDto
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Status { get; set; }
        public DateTime? ApprovalDate { get; set; }
    }

}
