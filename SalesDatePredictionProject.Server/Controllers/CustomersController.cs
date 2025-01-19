using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalesDatePredictionProject.Server.Dto;
using SalesDatePredictionProject.Server.Interfaces;

namespace SalesDatePredictionProject.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IMapper _mapper;

        public CustomersController(ICustomersRepository customersRepository,
            IMapper mapper)
        {
            _customersRepository = customersRepository;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetCustomerOrderPredictions")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OrderPredictionDto>))]
        public IActionResult Get()
        {
            var orderPredictions = _mapper.Map<List<OrderPredictionDto>>(_customersRepository.GetCustomerOrderPredictions());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orderPredictions);
        }
    }
}
