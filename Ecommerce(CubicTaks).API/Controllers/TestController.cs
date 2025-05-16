using Ecommerce_CubicTaks_.Application.Contract;
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
   
    public class TestController : ApiController
    {
        private readonly Itestserice itestserice;
        private readonly ICustomerRepository customerRepository;
        public TestController(Itestserice itestserice, ICustomerRepository customerRepository)
        {
            this.itestserice = itestserice;
            this.customerRepository = customerRepository;
        }
        [HttpGet]
       [Route("api/hello")]
        public IHttpActionResult SayHello()
        {
            return Ok("Hello from .NET Framework Web API!");
        }


    }
}
