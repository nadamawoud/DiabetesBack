using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Core.Entities
{
    public class ChatbotResultCasualUser : BaseEntity
    {           
            public string Result { get; set; }
            public DateTime ResultDate { get; set; }

        // Navigation property
        public int CasualUserID { get; set; }
        public CasualUser CasualUser { get; set; }
        
    }
}
