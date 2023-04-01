using CustomerAPI.Core.Data.Interface;
using CustomerAPI.Core.Entities;
using CustomerAPI.Core.Model;
using CustomerAPI.Core.Repository.Interface;


namespace CustomerAPI.Core.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private ICustomerStore customerStore { get; set; }
        public CustomerRepository(ICustomerStore customerStore) {
            this.customerStore = customerStore;
        }

        /// <summary>
        /// Create Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
       
        public async Task<CustomerResponse> CreateCustomer(Customer customer)
        {
            try
            {
                return await customerStore.Add(customer);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Delete customer
        /// </summary>
        /// <param name="customer"></param>
        
        public async Task DeleteCustomer(string id)
        {
            try
            {
                await customerStore.Delete(id);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get All Customters
        /// </summary>
        /// <returns></returns>
       
        public async Task<PaginationResponse<Customer>> GetAll(PaginationRequest request)
        {
            try
            {
                return await customerStore.GetAll(request);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get Customter by Id
        /// </summary>
        /// <returns></returns>

        public async Task<Customer> GetCustomer(string id)
        {
            try
            {
                return await customerStore.Get(id);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Update Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>

        public async Task<CustomerResponse> UpdateCustomer(Customer customer)
        {
            try
            {
                return await customerStore.Update(customer);
            }
            catch
            {
                throw;
            }
        }
    }
}
