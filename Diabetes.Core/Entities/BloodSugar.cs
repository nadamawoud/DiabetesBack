using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Core.Entities
{
    public class BloodSugar : BaseEntity
    {
            public string Period { get; set; }
            public double GlucoseLevel { get; set; }
            public DateTime MeasurementDate { get; set; }

        // Navigation property
        public int CasualUserID { get; set; }
        public CasualUser CasualUser { get; set; }        
    }
}
