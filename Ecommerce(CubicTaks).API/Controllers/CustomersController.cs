using Ecommerce_CubicTaks_.Application.Service;
using Ecommerce_CubicTaks_.Dto.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Ecommerce_CubicTaks_.API.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/customers
        [HttpGet]
        [Route("GetAllCustomers")]
        public async Task<IHttpActionResult> GetAll()
        {
            var customers = await _customerService.GetAllCustomers();
            return Ok(customers);
        }

        // GET: api/customers/{id}
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _customerService.GetCustomerById(id);
            if (!result.IsSuccess)
                return NotFound();

            return Ok(result.Entity);
        }

        // POST: api/customers
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateCustomerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _customerService.CreateCustomer(dto);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Created($"api/customers/{result.Entity.Id}", result.Entity);
        }

        // PUT: api/customers/{id}
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateCustomerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.Id)
                return BadRequest("Mismatched customer ID.");

            var result = await _customerService.UpdateCustomer(dto);
            if (!result.IsSuccess)
                return NotFound();

            return Ok(result.Entity);
        }

        // DELETE: api/customers/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var result = await _customerService.SoftDelete(id);
            if (!result.IsSuccess)
                return NotFound();

            return Ok(result.Entity);
        }
    }
}
