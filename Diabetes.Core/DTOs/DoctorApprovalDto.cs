using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Core.DTOs
{
    public class DoctorApprovalDTO
    {
        public string ApprovalStatus { get; set; } = "Pending";
    }
}
