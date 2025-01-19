using Microsoft.AspNetCore.Mvc;
using SalesDatePredictionProject.Server.Dto;
using SalesDatePredictionProject.Server.Interfaces;
using SalesDatePredictionProject.Server.Models;

namespace SalesDatePredictionProject.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrdersController(
            IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        [HttpGet("{custId}", Name = "GetOrdersByCustom")]
        [ProducesResponseType(200, Type = typeof(ICollection<Orders>))]
        [ProducesResponseType(400)]
        public IActionResult Get(int custId)
        {
            if (!_ordersRepository.CustomerExists(custId))
                return NotFound();

            var ordersByCustom = _ordersRepository.GetOrdersByCustom(custId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(ordersByCustom);
        }

        [HttpPost(Name = "CreateOrderWithProduct")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] OrderProductDto orderProduct)
        {
            if (orderProduct == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var OrderCreated = _ordersRepository.CreateOrderWithProduct(orderProduct);

            if (OrderCreated == default)
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created, orden con Id " + OrderCreated);
        }
    }
}
