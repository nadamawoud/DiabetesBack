using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Core.Entities
{
    public class Manager 
    {
        public int ID { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        // Navigation Property for Admin
        public int? AdminID { get; set; }
        public Admin Admin { get; set; }
    }
}
