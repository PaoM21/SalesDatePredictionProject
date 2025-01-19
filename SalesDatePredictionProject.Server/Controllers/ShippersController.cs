using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalesDatePredictionProject.Server.Interfaces;
using SalesDatePredictionProject.Server.Models;

namespace SalesDatePredictionProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippersController : Controller
    {
        private readonly IShippersRepository _shippersRepository;
        private readonly IMapper _mapper;

        public ShippersController(IShippersRepository shippersRepository,
            IMapper mapper)
        {
            _shippersRepository = shippersRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Shippers>))]
        public IActionResult GetShippers()
        {
            var shippers = _shippersRepository.GetShippers();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(shippers);
        }
    }
}
