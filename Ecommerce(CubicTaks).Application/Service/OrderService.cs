using AutoMapper;
using Ecommerce_CubicTaks_.Application.Contract;
using Ecommerce_CubicTaks_.Dto.Order;
using Ecommerce_CubicTaks_.Dto.ViewResult;
using Ecommerce_CubicTaks_.Model.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_CubicTaks_.Application.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerOrderRepository _customerOrderRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            IMapper mapper,
            ICustomerOrderRepository customerOrderRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
            _customerOrderRepository = customerOrderRepository;
        }

        public async Task<ResultView<OrderDto>> CreateOrder(CreateOrderDto orderDto)
        {
            var result = new ResultView<OrderDto>();

            var order = _mapper.Map<Order>(orderDto);

            // Save order first
            order = await _orderRepository.CreateAsync(order);

            // Create links to customers
            foreach (var customerId in orderDto.CustomerIds)
            {
                var customer = await _customerRepository.GetByIdAsync(customerId);
                if (customer != null && !customer.IsDeleted)
                {
                    await _customerOrderRepository.AddAsync(new CustomerOrder
                    {
                        OrderId = order.Id,
                        CustomerId = customer.Id
                    });
                }
            }

            await _customerOrderRepository.SaveChangesAsync();

            result.IsSuccess = true;
            result.Message = "Order created successfully.";
            result.Entity = _mapper.Map<OrderDto>(order);
            return result;
        }

        public async Task<ResultView<OrderDto>> UpdateOrder(UpdateOrderDto orderDto)
        {
            var result = new ResultView<OrderDto>();

            var existing = await _orderRepository.GetByIdAsync(orderDto.Id);
            if (existing == null || existing.IsDeleted)
            {
                result.IsSuccess = false;
                result.Message = "Order not found.";
                return result;
            }

            _mapper.Map(orderDto, existing);
            await _orderRepository.UpdateAsync(existing);

            // Remove existing customer-order links
            var existingLinks = await _customerOrderRepository.GetByOrderIdAsync(existing.Id);
            foreach (var link in existingLinks)
            {
                await _customerOrderRepository.RemoveAsync(link.CustomerId, link.OrderId);
            }

            // Add updated links
            foreach (var customerId in orderDto.CustomerIds)
            {
                var customer = await _customerRepository.GetByIdAsync(customerId);
                if (customer != null && !customer.IsDeleted)
                {
                    await _customerOrderRepository.AddAsync(new CustomerOrder
                    {
                        OrderId = existing.Id,
                        CustomerId = customer.Id
                    });
                }
            }

            await _customerOrderRepository.SaveChangesAsync();

            result.IsSuccess = true;
            result.Message = "Order updated successfully.";
            result.Entity = _mapper.Map<OrderDto>(existing);
            return result;
        }

        public async Task<ResultView<OrderDto>> GetOrderById(int id)
        {
            var result = new ResultView<OrderDto>();

            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null || order.IsDeleted)
            {
                result.IsSuccess = false;
                result.Message = "Order not found.";
                return result;
            }

            result.IsSuccess = true;
            result.Entity = _mapper.Map<OrderDto>(order);
            return result;
        }

        public async Task<ICollection<OrderDto>> GetAllOrders()
        {
            var all = await _orderRepository.GetAllAsync();

            // Materialize first to List
            var list = await all
                .Where(o => !o.IsDeleted)
                .ToListAsync(); // ✅ SQL executed here

            // Now map in memory
            return list
                .Select(o => _mapper.Map<OrderDto>(o)) // ✅ Safe now
                .ToList();
        }


        public async Task<ResultDataList<OrderDto>> GetAllPagination(int items, int pageNumber)
        {
            var all = await _orderRepository.GetAllAsync();
            var filtered = all.Where(o => !o.IsDeleted);

            var paged = filtered
                .OrderByDescending(o => o.Id)
                .Skip((pageNumber - 1) * items)
                .Take(items)
                .ToList();

            var dtoList = paged.Select(o => _mapper.Map<OrderDto>(o)).ToList();

            return new ResultDataList<OrderDto>(dtoList, filtered.Count());
        }

        public async Task<ResultView<OrderDto>> SoftDelete(int orderId)
        {
            var result = new ResultView<OrderDto>();

            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null || order.IsDeleted)
            {
                result.IsSuccess = false;
                result.Message = "Order not found.";
                return result;
            }

            order.IsDeleted = true;
            await _orderRepository.UpdateAsync(order);

            result.IsSuccess = true;
            result.Message = "Order soft deleted.";
            result.Entity = _mapper.Map<OrderDto>(order);
            return result;
        }
    }

}
