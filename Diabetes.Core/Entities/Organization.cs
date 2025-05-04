using Diabetes.Core.Entities;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Core.Entities
{
    public class Organization 
    {
        public int ID { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public bool IsMedicalSyndicate { get; set; }
        // Navigation properties
        public int AdminID { get; set; }
        public Admin Admin { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<DoctorApproval> DoctorApprovals { get; set; }
    }
}
