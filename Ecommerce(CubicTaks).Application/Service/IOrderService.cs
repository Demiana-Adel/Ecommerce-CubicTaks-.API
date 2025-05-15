using Ecommerce_CubicTaks_.Dto.Order;
using Ecommerce_CubicTaks_.Dto.ViewResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_CubicTaks_.Application.Service
{
    public interface IOrderService
    {
        Task<ICollection<OrderDto>> GetAllOrders();
        Task<ResultDataList<OrderDto>> GetAllPagination(int items, int pageNumber);
        Task<ResultView<OrderDto>> CreateOrder(CreateOrderDto orderDto);
        Task<ResultView<OrderDto>> UpdateOrder(UpdateOrderDto orderDto);
        Task<ResultView<OrderDto>> GetOrderById(int id);
        Task<ResultView<OrderDto>> SoftDelete(int orderId);

       
    }
}
