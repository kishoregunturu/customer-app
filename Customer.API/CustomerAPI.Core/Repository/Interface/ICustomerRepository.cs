using CustomerAPI.Core.Entities;
using CustomerAPI.Core.Model;

namespace CustomerAPI.Core.Repository.Interface
{
    public interface ICustomerRepository
    {
        Task<PaginationResponse<Customer>> GetAll(PaginationRequest request);

        Task<Customer> GetCustomer(string id);
        Task<CustomerResponse> CreateCustomer(Customer customer);
        Task<CustomerResponse> UpdateCustomer(Customer customer);     
        Task DeleteCustomer(string id);
    }
}
