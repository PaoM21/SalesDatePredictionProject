using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using SalesDatePredictionProject.Server.Controllers;
using SalesDatePredictionProject.Server.Dto;
using SalesDatePredictionProject.Server.Interfaces;

namespace SalesDatePredictionProject.Tests.Controllers
{
    public class CustomersControllerTest
    {
        private ICustomersRepository _customersRepository { get; set; }
        private IMapper _mapper { get; set; }
        private CustomersController _customersController { get; set; }
        public CustomersControllerTest()
        {
            //Dependencies
            _customersRepository = A.Fake<ICustomersRepository>();
            _mapper = A.Fake<IMapper>();

            //SUT
            _customersController = new CustomersController(_customersRepository, _mapper);
        }

        [Fact]
        public void CustomersController_GetCustomers_ReturnsSuccess()
        {
            // Arrange
            var customers = A.Fake<IEnumerable<OrderPredictionDto>>();
            A.CallTo(() => _customersRepository.GetCustomerOrderPredictions()).Returns(customers);

            // Act
            var result = _customersController.Get();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
