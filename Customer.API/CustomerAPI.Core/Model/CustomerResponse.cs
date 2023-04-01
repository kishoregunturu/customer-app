using CustomerAPI.Core.Entities;

namespace CustomerAPI.Core.Model
{
    public class CustomerResponse
    {
        public Customer? Customer { get; set; }
        public List<string>? Errors { get; set; }
    }
}
