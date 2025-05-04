using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Core.Entities
{
    public class FoodItem : BaseEntity
    {            
            public string FoodName { get; set; }
            public int GlycemicIndex { get; set; }
            public string GlycemicCategory { get; set; }
            // Navigation property
            public ICollection<SuggestedFood> SuggestedFoods { get; set; }        
    }
}
