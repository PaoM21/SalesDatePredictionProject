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

        public CustomersController(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        [HttpGet(Name = "GetCustomerOrderPredictions")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OrderPredictionDto>))]
        public IActionResult Get()
        {
            var orderPredictions = _customersRepository.GetCustomerOrderPredictions();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orderPredictions);
        }
    }
}
