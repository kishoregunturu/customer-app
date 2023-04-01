using System.ComponentModel.DataAnnotations;

namespace CustomerAPI.Core.Entities
{
    public class Customer:BaseEntity 
    {
        [Key]
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
}