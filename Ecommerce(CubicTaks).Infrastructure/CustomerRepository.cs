using Ecommerce_CubicTaks_.Application.Contract;
using Ecommerce_CubicTaks_.Context;
using Ecommerce_CubicTaks_.Model.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_CubicTaks_.Infrastructure
{

    public class CustomerRepository : Repository<Customer, int>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Customer> GetCustomerByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.Email == email && !c.IsDeleted);
        }

        public async Task<List<Customer>> GetCustomersWithOrdersAsync()
        {
            return await _dbSet
                .Include(c => c.CustomerOrders.Select(co => co.Order))
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }
    }
}
