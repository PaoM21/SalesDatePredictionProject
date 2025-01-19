using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalesDatePredictionProject.Server.Interfaces;
using SalesDatePredictionProject.Server.Models;

namespace SalesDatePredictionProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductsRepository productsRepository,
            IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Products>))]
        public IActionResult GetProducts()
        {
            var products = _productsRepository.GetProducts();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(products);
        }
    }
}
