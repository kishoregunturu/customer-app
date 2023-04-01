using CustomerAPI.Core.Data.Interface;
using CustomerAPI.Core.Entities;
using CustomerAPI.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CustomerAPI.Core.Data
{
    public class CustomerStore : ICustomerStore
    {
        private IConfiguration configuration { get; set; }
        public CustomerStore(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Add customer to thestore
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<CustomerResponse> Add(Customer customer)
        {
            try
            {
                var response = new CustomerResponse();

                response.Errors = ValidateCustomer(customer);
                if (response.Errors == null || response.Errors.Count == 0)
                {
                    customer.CreatedOn = DateTime.UtcNow;
                    customer.UpdatedOn = DateTime.UtcNow;
                    using (var db = new CustomerContext(configuration))
                    {
                        //check if user with email already exists
                        var dbCustomer = db.Customers.SingleOrDefault(c => !string.IsNullOrEmpty(c.Email)
                        && !string.IsNullOrEmpty(customer.Email)
                        && c.Email.ToLower()
                        == customer.Email.ToLower());
                        if (dbCustomer == null)
                        {
                            await db.Customers.AddAsync(customer);
                            await db.SaveChangesAsync();
                        }
                        else
                        {
                            response.Errors = new List<string>() { "Customer with email already exists" };
                        }
                    }
                    response.Customer = customer;
                }

                return response;
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
        public async Task Delete(string id)
        {
            try
            {


                using (var db = new CustomerContext(configuration))
                {
                    var customer = await db.Customers.FirstOrDefaultAsync(c => c.Id == id);
                    if (customer == null)
                        throw new FileNotFoundException("No customer exists");
                    db.Customers.Remove(customer);
                    await db.SaveChangesAsync();
                }

            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get customer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Customer> Get(string id)
        {
            try
            {
                Customer customer;

                using (var db = new CustomerContext(configuration))
                {
                    customer = await db.Customers.SingleAsync(c => c.Id == id);

                }
                if (customer == null)
                    throw new FileNotFoundException("No Customer exists");
                return customer;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get All Customers
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<PaginationResponse<Customer>> GetAll(PaginationRequest request)
        {
            try
            {
                using (var db = new CustomerContext(configuration))
                {
                    var customers = await db.Customers.ToListAsync();
                    var data = new List<Customer>();

                    if (customers != null)
                    {
                        //Ideal, this should be done in a sproc or data layer
                        //the Totals and dynamic sql to optimize
                        //I did it here since it is a demo and I don't want to over do it
                        var total = customers.Count;
                        if (request.PerPage == 0)
                            request.PerPage = 10;
                        if (request.PerPage >= total)
                            data = customers;
                        else
                            data = customers.OrderBy(t => t.LastName).ThenBy(t => t.FirstName)
                                .Skip<Customer>(request.PerPage * (request.Page - 1))
                                .Take<Customer>(request.PerPage).ToList();
                        decimal lastPage = (decimal)total/request.PerPage;
                        return new PaginationResponse<Customer>
                        {
                            Data = data,
                            Total = total,
                            CurrentPage = request.Page,
                            LastPage = (int)Math.Ceiling(lastPage) ,

                            PerPage = request.PerPage
                        };
                    }
                    else
                        return new PaginationResponse<Customer>();
                    

                }

            }
            catch
            {
                throw;
            }


        }

        /// <summary>
        /// Update customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<CustomerResponse> Update(Customer customer)
        {
            try
            {
                var response = new CustomerResponse();

                response.Errors = ValidateCustomer(customer);
                if (response.Errors == null || response.Errors.Count == 0)
                {
                    customer.UpdatedOn = DateTime.UtcNow;
                    using (var db = new CustomerContext(configuration))
                    {
                        var dbCustomer = await db.Customers.SingleOrDefaultAsync(c => c.Id == customer.Id);
                        if (dbCustomer == null)
                            response.Errors = new List<string> { "No customer exists" };
                        else
                        {
                            //check if another user has same email
                            var dbDuplicateUser = await db.Customers.SingleOrDefaultAsync(c => c.Id != customer.Id
                            && !string.IsNullOrEmpty(c.Email)
                            && !string.IsNullOrEmpty(customer.Email)
                            && c.Email.ToLower()
                            == customer.Email.ToLower());

                            if (dbDuplicateUser == null)
                            {
                                dbCustomer.FirstName = customer.FirstName;
                                dbCustomer.LastName = customer.LastName;
                                dbCustomer.Email = customer.Email;
                                db.Customers.Update(dbCustomer);
                                await db.SaveChangesAsync();
                            }
                            else
                            {
                                response.Errors = new List<string> { "Another customer has the same email" };
                            }
                        }
                        response.Customer = dbCustomer;
                    }

                }
                return response;
            }
            catch
            {
                throw;
            }
        }

        #region "Private"
        /// <summary>
        /// Validate customer
        /// </summary>
        /// <param name="customer"></param>
        private List<string> ValidateCustomer(Customer customer)
        {
            var errors = new List<string>();
            if (customer == null)
                errors.Add("Customer cannot be null");
            else
            {
                if (string.IsNullOrEmpty(customer.FirstName))
                    errors.Add("First Name is required");
                if (string.IsNullOrEmpty(customer.LastName))
                    errors.Add("Last Name is required");
                if (string.IsNullOrEmpty(customer.Email))
                    errors.Add("Email is required");
                else
                {
                    var email = new EmailAddressAttribute();
                    if (!email.IsValid(customer.Email))
                        errors.Add("Valid Email is required");
                }
            }

            return errors;
        }
        #endregion
    }
}
