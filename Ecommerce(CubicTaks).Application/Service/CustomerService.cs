using AutoMapper;
using Ecommerce_CubicTaks_.Application.Contract;
using Ecommerce_CubicTaks_.Dto.Customer;
using Ecommerce_CubicTaks_.Dto.ViewResult;
using Ecommerce_CubicTaks_.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_CubicTaks_.Application.Service
{

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<ResultView<CustomerDto>> CreateCustomer(CreateCustomerDto dto)
        {
            var result = new ResultView<CustomerDto>();

            var customer = _mapper.Map<Customer>(dto);
            var created = await _customerRepository.CreateAsync(customer);

            result.IsSuccess = true;
            result.Message = "Customer created successfully.";
            result.Entity = _mapper.Map<CustomerDto>(created);
            return result;
        }

        public async Task<ResultView<CustomerDto>> UpdateCustomer(UpdateCustomerDto dto)
        {
            var result = new ResultView<CustomerDto>();

            var existing = await _customerRepository.GetByIdAsync(dto.Id);
            if (existing == null || existing.IsDeleted)
            {
                result.IsSuccess = false;
                result.Message = "Customer not found.";
                return result;
            }

            _mapper.Map(dto, existing);
            var updated = await _customerRepository.UpdateAsync(existing);

            result.IsSuccess = true;
            result.Message = "Customer updated successfully.";
            result.Entity = _mapper.Map<CustomerDto>(updated);
            return result;
        }

        public async Task<ResultView<CustomerDto>> GetCustomerById(int id)
        {
            var result = new ResultView<CustomerDto>();

            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null || customer.IsDeleted)
            {
                result.IsSuccess = false;
                result.Message = "Customer not found.";
                return result;
            }

            result.IsSuccess = true;
            result.Entity = _mapper.Map<CustomerDto>(customer);
            return result;
        }

        public async Task<ICollection<CustomerDto>> GetAllCustomers()
        {
            var all = await _customerRepository.GetAllAsync();
            return all
                .Where(c => !c.IsDeleted)
                .Select(c => _mapper.Map<CustomerDto>(c))
                .ToList();
        }

        public async Task<ResultDataList<CustomerDto>> GetAllPagination(int items, int pageNumber)
        {
            var all = await _customerRepository.GetAllAsync();
            var filtered = all.Where(c => !c.IsDeleted);

            var paged = filtered
                .OrderByDescending(c => c.Id)
                .Skip((pageNumber - 1) * items)
                .Take(items)
                .ToList();

            var dtoList = paged.Select(c => _mapper.Map<CustomerDto>(c)).ToList();

            return new ResultDataList<CustomerDto>(dtoList, filtered.Count());
        }

        public async Task<ResultView<CustomerDto>> SoftDelete(int customerId)
        {
            var result = new ResultView<CustomerDto>();

            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null || customer.IsDeleted)
            {
                result.IsSuccess = false;
                result.Message = "Customer not found.";
                return result;
            }

            customer.IsDeleted = true;
            await _customerRepository.UpdateAsync(customer);

            result.IsSuccess = true;
            result.Message = "Customer soft deleted.";
            result.Entity = _mapper.Map<CustomerDto>(customer);
            return result;
        }
    }

}
