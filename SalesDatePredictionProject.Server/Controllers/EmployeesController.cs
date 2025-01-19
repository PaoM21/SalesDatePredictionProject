using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalesDatePredictionProject.Server.Interfaces;
using SalesDatePredictionProject.Server.Models;

namespace SalesDatePredictionProject.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeesRepository employeesRepository,
            IMapper mapper)
        {
            _employeesRepository = employeesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Employees>))]
        public IActionResult GetEmployees()
        {
            var employees = _employeesRepository.GetEmployees();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employees);
        }
    }
}
