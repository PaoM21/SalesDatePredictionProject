using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalesDatePredictionProject.Server.Dto;
using SalesDatePredictionProject.Server.Interfaces;
using SalesDatePredictionProject.Server.Models;

namespace SalesDatePredictionProject.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpGet(Name = "GetShippers")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ShipperDto>))]
        public IActionResult Get()
        {
            var shippers = _mapper.Map<List<ShipperDto>>(_shippersRepository.GetShippers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(shippers);
        }
    }
}
