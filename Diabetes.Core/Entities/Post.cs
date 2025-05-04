using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Core.Entities
{
    public class Post : BaseEntity
    {
            public string Title { get; set; }
            public string Content { get; set; }
            public string ImageURL { get; set; }
            public string VideoURL { get; set; }
            public DateTime PublishDate { get; set; }
        // Navigation property
        public int OrganizationID { get; set; }
        public Organization Organization { get; set; }        
    }
}

