using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Core.Entities
{
    public class SuggestedFood : BaseEntity
    {
          public DateTime SuggestedDate { get; set; }
        // Navigation properties
            public int PatientID { get; set; }
            public int FoodItemID { get; set; }
            public int DoctorID { get; set; }
            public Patient Patient { get; set; }
            public FoodItem FoodItem { get; set; }
            public Doctor Doctor { get; set; }        
    }
}
