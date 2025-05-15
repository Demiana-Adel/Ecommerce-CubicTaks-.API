using Ecommerce_CubicTaks_.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_CubicTaks_.Application.Contract
{
    public interface ICustomerOrderRepository
    {
        Task<CustomerOrder> AddAsync(CustomerOrder customerOrder);
        Task RemoveAsync(int customerId, int orderId);
        Task<List<CustomerOrder>> GetByCustomerIdAsync(int customerId);
        Task<List<CustomerOrder>> GetByOrderIdAsync(int orderId);
        Task<bool> ExistsAsync(int customerId, int orderId);
        Task<int> SaveChangesAsync();
    }
}
