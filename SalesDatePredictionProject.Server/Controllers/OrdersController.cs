using AutoMapper;
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
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;

        public OrdersController(IOrdersRepository ordersRepository,
            IProductsRepository productsRepository,
            IMapper mapper)
        {
            _ordersRepository = ordersRepository;
            _productsRepository = productsRepository;
            _mapper = mapper;
        }

        [HttpGet("{custId}", Name = "GetOrdersByCustom")]
        [ProducesResponseType(200, Type = typeof(ICollection<OrderDto>))]
        [ProducesResponseType(400)]
        public IActionResult Get(int custId)
        {
            if (!_ordersRepository.CustomerExists(custId))
                return NotFound();

            var ordersByCustom = _ordersRepository.GetOrdersByCustom(custId);
            var ordersDto = _mapper.Map<ICollection<OrderDto>>(ordersByCustom);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(ordersDto);
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
