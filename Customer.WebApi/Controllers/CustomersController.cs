namespace Customers.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Customers.Persistence.Models;
    using Customers.Persistence.Models.DataModels;
    using Customers.Persistence.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Linq;

    [ApiController]
    [Route("Customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService customerService;
        private readonly IMapper mapper;

        public CustomersController(ICustomersService customerService, IMapper mapper)
        {
            this.customerService = customerService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CustomersViewModel>>> GetCustomers()
        {
            var customers = this.mapper.Map<List<Customer>, List<CustomersViewModel>>(await this.customerService.GetCustomers());
            if (customers == null)
            {
                return this.NoContent();
            }

            return customers;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<CustomersViewModel>> GetCustomer(int id)
        {
            var customer = this.mapper.Map<Customer, CustomersViewModel>(await this.customerService.GetCustomer(id));
            if (customer == null)
            {
                return this.NotFound();
            }

            return customer;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateNewCustomer([FromBody] JObject data)
        {
            if (data == null)
            {
                return this.BadRequest();
            }

            if (!data.ContainsKey("customerName") || !data.ContainsKey("managerId"))
            {
                return this.BadRequest();
            }

            var customerName = data["customerName"].ToString();
            var managerId = int.Parse(data["managerId"].ToString());

            await this.customerService.CreateCustomer(customerName, managerId);
            return this.Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await this.customerService.DeleteCustomer(id);
            return this.Ok();
        }
    }
}