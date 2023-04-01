using CustomerAPI.Core.Data.Interface;
using CustomerAPI.Core.Entities;
using CustomerAPI.Core.Model;
using CustomerAPI.Core.Repository;
using Moq;

namespace ConsumerAPI.Test
{
    [TestClass]
    public class CustomerRespositoryTest
    {
        private CustomerRepository customerRepository { get; set; }

        [TestInitialize]
        public void Init()
        {
            var mockingStore = new Mock<ICustomerStore>();
            mockingStore.Setup(p => p.GetAll(It.IsAny<PaginationRequest>()))
                .ReturnsAsync(new PaginationResponse<Customer> { Data = new List<Customer>() });
            mockingStore.Setup(p => p.Add(It.IsAny<Customer>()))
                .ReturnsAsync(new CustomerResponse());
            mockingStore.Setup(p => p.Update(It.IsAny<Customer>()))
                .ReturnsAsync(new CustomerResponse());
            mockingStore.Setup(p => p.Delete(It.IsAny<string>()));
            customerRepository = new CustomerRepository(mockingStore.Object);
        }

        [TestMethod]
        public async void TestGetAll()
        {

            var customers = await customerRepository.GetAll(new PaginationRequest());
            Assert.IsNotNull(customers.Data);
        }

        [TestMethod]
        public async void TestCreate()
        {
            var customer = await customerRepository.CreateCustomer(new Customer());
            Assert.IsNotNull(customer);
        }

        [TestMethod]
        public async void TestCreateCustomerIsNull()
        {
            bool exception = false;
            try
            {
                var customer = await customerRepository.CreateCustomer(null);
            }
            catch
            {
                exception = true;
            }
           Assert.IsTrue(exception);
        }

        [TestMethod]
        public async void TestUpdate()
        {
            var customer = await customerRepository.UpdateCustomer(new Customer());
            Assert.IsNotNull(customer);
        }

        [TestMethod]
        public async void TestUpdateCustomerIsNull()
        {
            bool exception = false;
            try
            {
                var customer = await customerRepository.UpdateCustomer(null);
            }
            catch
            {
                exception = true;
            }
            Assert.IsTrue(exception);
        }
    }
}