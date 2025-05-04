using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diabetes.Core.Entities
{
    public class BaseEntity
    {
        public int ID { get; set; }
    }
}