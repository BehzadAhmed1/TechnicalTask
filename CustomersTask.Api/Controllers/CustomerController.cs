using CustomersTask.Infrastructure.Contracts;
using CustomersTask.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Customers.Api.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return Ok(await _customerRepository.GetCustomersAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> AddCustomer(Customer customer)
        {
            if (customer == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _customerRepository.AddCustomerAsync(customer);
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(Guid id)
        {
            var customer = await _customerRepository.GetCustomerAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult<Customer>> UpdateCustomer(Guid id, Customer customer)
        //{
        //    if (id != customer.Id)
        //    {
        //        return BadRequest("The Id in the URL does not match the Id of the customer.");
        //    }
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    await _customerRepository.UpdateCustomerAsync(customer);

        //    return Ok(customer);
        //}


        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> UpdateCustomer(Guid id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest("The Id in the URL does not match the Id of the customer.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCustomer = await _customerRepository.GetCustomerAsync(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;

            await _customerRepository.UpdateCustomerAsync(existingCustomer);

            return Ok(existingCustomer);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(Guid id)
        {
            var customer = await _customerRepository.GetCustomerAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            await _customerRepository.DeleteCustomerAsync(id);

            return Ok(customer);
        }
    }

}