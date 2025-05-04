using Diabetes.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Core.Entities
{
    public class Alarm : BaseEntity
    {           
            public string AlarmType { get; set; }
            public DateTime AlarmTime { get; set; }

        // Navigation property
        public int CasualUserID { get; set; }
        public CasualUser CasualUser { get; set; }       
    }
}
