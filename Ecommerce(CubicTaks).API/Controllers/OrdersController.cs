using System.Threading.Tasks;
using System.Web.Http;
using Ecommerce_CubicTaks_.Application.Service;
using Ecommerce_CubicTaks_.Dto.Order;

namespace Ecommerce_CubicTaks_.API.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrdersController : ApiController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/orders
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAll()
        {
            var result = await _orderService.GetAllOrders();
            return Ok(result);
        }

        // GET: api/orders/paginated?items=10&pageNumber=1
        [HttpGet]
        [Route("paginated")]
        public async Task<IHttpActionResult> GetPaginated(int items, int pageNumber)
        {
            var result = await _orderService.GetAllPagination(items, pageNumber);
            return Ok(result);
        }

        // GET: api/orders/5
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _orderService.GetOrderById(id);
            if (!result.IsSuccess)
                return NotFound();

            return Ok(result.Entity);
        }

        // POST: api/orders
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateOrderDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _orderService.CreateOrder(dto);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Created($"api/orders/{result.Entity.Id}", result.Entity);
        }

        // PUT: api/orders/5
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateOrderDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.Id)
                return BadRequest("Mismatched order ID.");

            var result = await _orderService.UpdateOrder(dto);
            if (!result.IsSuccess)
                return NotFound();

            return Ok(result.Entity);
        }

        // DELETE: api/orders/5
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var result = await _orderService.SoftDelete(id);
            if (!result.IsSuccess)
                return NotFound();

            return Ok(result.Entity);
        }
    }
}
