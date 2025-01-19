using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using SalesDatePredictionProject.Server.Controllers;
using SalesDatePredictionProject.Server.Dto;
using SalesDatePredictionProject.Server.Interfaces;
using SalesDatePredictionProject.Server.Models;

namespace SalesDatePredictionProject.Tests.Controllers
{
    public class EmployeesControllerTest
    {
        private IEmployeesRepository _employeesRepository { get; set; }
        private IMapper _mapper { get; set; }
        private EmployeesController _employeesController { get; set; }
        public EmployeesControllerTest()
        {
            //Dependencies
            _mapper = A.Fake<IMapper>();
            _employeesRepository = A.Fake<IEmployeesRepository>();

            //SUT
            _employeesController = new EmployeesController(_employeesRepository, _mapper);
        }

        [Fact]
        public void EmployeesController_GetEmployees_ReturnsSuccess()
        {
            // Arrange
            var employees = A.Fake<IEnumerable<EmployeeDto>>();
            A.CallTo(() => _mapper.Map<List<EmployeeDto>>(_employeesRepository.GetEmployees()));

            // Act
            var result = _employeesController.Get();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

    }
}
