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
    public class OrderRepository : Repository<Order, int>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _context.CustomerOrders
                .Where(co => co.CustomerId == customerId && !co.Order.IsDeleted)
                .Select(co => co.Order)
                .ToListAsync();
        }
    }
}
