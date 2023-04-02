
using CustomerAPI.Core.Entities;
using CustomerAPI.Core.Model;
using CustomerAPI.Core.Repository.Interface;
using CustomerAPI.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CustomerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {

        private readonly ILogger<LoginController> _logger;
        private IConfiguration configuration;

        public LoginController(ILogger<LoginController> logger,
            IConfiguration configuraton)
        {
            _logger = logger;
            this.configuration = configuraton;
            
        }

        /// <summary>
        /// Get All Customers
        /// </summary>
        /// <returns></returns>
        [HttpPost]
       
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Customer>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public  IActionResult Login()
        {
            var method = "GetCustomers";
            try
            {
                
                _logger.Log(LogLevel.Information, $"Executing {method}");
                var jwt = new JWTHelper(configuration);
                return this.StatusCode(200,jwt.GenerateToken());
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

    }
}