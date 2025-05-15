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
    public class CustomerOrderRepository : ICustomerOrderRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly DbSet<CustomerOrder> _dbSet;

        public CustomerOrderRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.CustomerOrders;
        }

        public async Task<CustomerOrder> AddAsync(CustomerOrder customerOrder)
        {
            _dbSet.Add(customerOrder);
            await _context.SaveChangesAsync();
            return customerOrder;
        }

        public async Task RemoveAsync(int customerId, int orderId)
        {
            var entity = await _dbSet
                .FirstOrDefaultAsync(co => co.CustomerId == customerId && co.OrderId == orderId);

            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<CustomerOrder>> GetByCustomerIdAsync(int customerId)
        {
            return await _dbSet
                .Where(co => co.CustomerId == customerId)
                .Include(co => co.Order)
                .ToListAsync();
        }

        public async Task<List<CustomerOrder>> GetByOrderIdAsync(int orderId)
        {
            return await _dbSet
                .Where(co => co.OrderId == orderId)
                .Include(co => co.Customer)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int customerId, int orderId)
        {
            return await _dbSet.AnyAsync(co =>
                co.CustomerId == customerId && co.OrderId == orderId);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
