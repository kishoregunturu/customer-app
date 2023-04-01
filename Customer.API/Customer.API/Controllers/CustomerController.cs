
using CustomerAPI.Core.Entities;
using CustomerAPI.Core.Model;
using CustomerAPI.Core.Repository.Interface;
using Microsoft.AspNetCore.Mvc;


namespace CustomerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {

        private readonly ILogger<CustomerController> _logger;
        private ICustomerRepository _customerRepository;

        public CustomerController(ILogger<CustomerController> logger,
            ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Get All Customers
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("All")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Customer>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomers([FromBody] PaginationRequest request)
        {
            var method = "GetCustomers";
            try
            {
                
                _logger.Log(LogLevel.Information, $"Executing {method}");
                var customers = await _customerRepository.GetAll(request);
                return this.StatusCode(200,customers);
            }catch(Exception ex)
            {
                _logger.Log(LogLevel.Error,$"Error while executing the {method}.", ex.Message);
                return this.StatusCode(500, ex.Message);
            }
            finally
            {
                _logger.Log(LogLevel.Information, $"Completed executing the {method}.");
            }
           
        }

        /// <summary>
        /// Get All Customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Customer>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomer(string id)
        {
            var method = "GetCustomers";
            try
            {
                _logger.Log(LogLevel.Information, $"Executing {method}");
                return this.StatusCode(200, await _customerRepository.GetCustomer(id));
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while executing the {method}.", ex.Message);
                return this.StatusCode(500, ex.Message);
            }
            finally
            {
                _logger.Log(LogLevel.Information, $"Completed executing the {method}.");
            }

        }
        /// <summary>
        /// Create Customer
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Customer))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            var method = "CreateCustomer";
            try
            {
                _logger.Log(LogLevel.Information, $"Executing {method}");
                return this.StatusCode(201,await _customerRepository.CreateCustomer(customer));
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while executing the {method}.", ex.Message);
                return this.StatusCode(500, ex.Message);
            }
            finally
            {
                _logger.Log(LogLevel.Information, $"Completed executing the {method}.");
            }
        }

        /// <summary>
        /// Update Customer
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Customer))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
        {
            var method = "UpdateCustomer";
            try
            {
                _logger.Log(LogLevel.Information, $"Executing {method}");
                return this.StatusCode(200,await _customerRepository.UpdateCustomer(customer));
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while executing the {method}.", ex.Message);
                return this.StatusCode(500, ex.Message);
            }
            finally
            {
                _logger.Log(LogLevel.Information, $"Completed executing the {method}.");
            }
        }

        /// <summary>
        /// Delete Customer
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            var method = "DeleteCustomer";
            try
            {
                _logger.Log(LogLevel.Information, $"Executing {method}");
                await this._customerRepository.DeleteCustomer(id);
                return this.StatusCode(204);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while executing the {method}.", ex.Message);
                return this.StatusCode(500, ex.Message);
            }
            finally
            {
                _logger.Log(LogLevel.Information, $"Completed executing the {method}.");
            }
        }
    }
}