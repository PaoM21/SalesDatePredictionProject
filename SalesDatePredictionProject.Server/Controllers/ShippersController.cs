using Microsoft.AspNetCore.Mvc;
using SalesDatePredictionProject.Server.Interfaces;
using SalesDatePredictionProject.Server.Models;

namespace SalesDatePredictionProject.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippersController : Controller
    {
        private readonly IShippersRepository _shippersRepository;

        public ShippersController(IShippersRepository shippersRepository)
        {
            _shippersRepository = shippersRepository;
        }

        [HttpGet(Name = "GetShippers")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Shippers>))]
        public IActionResult Get()
        {
            var shippers = _shippersRepository.GetShippers();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(shippers);
        }
    }
}
