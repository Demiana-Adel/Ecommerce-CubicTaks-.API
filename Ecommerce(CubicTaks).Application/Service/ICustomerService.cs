using Ecommerce_CubicTaks_.Dto.Customer;
using Ecommerce_CubicTaks_.Dto.ViewResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_CubicTaks_.Application.Service
{
    public interface ICustomerService
    {
        Task<ICollection<CustomerDto>> GetAllCustomers();
        Task<ResultDataList<CustomerDto>> GetAllPagination(int items, int pageNumber);
        Task<ResultView<CustomerDto>> CreateCustomer(CreateCustomerDto dto);
        Task<ResultView<CustomerDto>> UpdateCustomer(UpdateCustomerDto dto);
        Task<ResultView<CustomerDto>> GetCustomerById(int id);
        Task<ResultView<CustomerDto>> SoftDelete(int customerId);

    }
}
