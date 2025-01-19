using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalesDatePredictionProject.Server.Dto;
using SalesDatePredictionProject.Server.Interfaces;
using SalesDatePredictionProject.Server.Models;
using SalesDatePredictionProject.Server.Repository;

namespace SalesDatePredictionProject.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;

        public OrdersController(
            IOrdersRepository ordersRepository,
            IProductsRepository productsRepository,
            IMapper mapper)
        {
            _ordersRepository = ordersRepository;
            _productsRepository = productsRepository;
            _mapper = mapper;
        }

        [HttpGet("{custId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Orders>))]
        [ProducesResponseType(400)]
        public IActionResult GetOrdersByCustom(int custId)
        {
            if (!_ordersRepository.CustomerExists(custId))
                return NotFound();

            var ordersByCustom = _ordersRepository.GetOrdersByCustom(custId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(ordersByCustom);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOrderWithProduct([FromBody] OrderProductDto orderProduct)
        {
            if (orderProduct == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var OrderCreated = _ordersRepository.CreateOrderWithProduct(orderProduct);

            if (OrderCreated == null || OrderCreated == default(int))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created, orden con Id " + OrderCreated);
        }
    }
}
