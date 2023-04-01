using CustomerAPI.Core.Entities;
using CustomerAPI.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAPI.Core.Data.Interface
{
    public interface ICustomerStore
    {
        Task<PaginationResponse<Customer>> GetAll(PaginationRequest request);
        Task<Customer> Get(string id);

        Task<CustomerResponse> Add(Customer customer);
        Task<CustomerResponse> Update(Customer customer);
        Task Delete(string id);
    }
}
