using System.ComponentModel.DataAnnotations;

namespace CustomerAPI.Core.Entities
{
    public class BaseEntity
    {
       
        public DateTime CreatedOn { get; set; }
      
        public DateTime UpdatedOn { get; set; }
    }
}