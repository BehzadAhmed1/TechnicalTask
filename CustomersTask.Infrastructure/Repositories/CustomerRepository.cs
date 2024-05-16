using CustomersTask.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomersTask.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomersTask.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _context;

        public CustomerRepository(CustomerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            try
            {
                return await _context.Customers.ToListAsync();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Customer> GetCustomerAsync(Guid id)
        {
            try
            {
                return await _context.Customers.FindAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
       
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {

            try
            {
                _context.Entry(customer).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task DeleteCustomerAsync(Guid id)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(id);
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
         
        }
    }
}
