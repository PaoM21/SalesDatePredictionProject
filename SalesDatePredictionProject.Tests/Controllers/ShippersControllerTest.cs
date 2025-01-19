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
    public class ShippersControllerTest
    {
        private IShippersRepository _shippersRepository { get; set; }
        private IMapper _mapper { get; set; }
        private ShippersController _shippersController { get; set; }
        public ShippersControllerTest()
        {
            //Dependencies
            _mapper = A.Fake<IMapper>();
            _shippersRepository = A.Fake<IShippersRepository>();

            //SUT
            _shippersController = new ShippersController(_shippersRepository, _mapper);
        }

        [Fact]
        public void ShippersController_GetShippers_ReturnsSuccess()
        {
            // Arrange
            var shippers = A.Fake<ICollection<ShipperDto>>();
            var shippersModel = A.Fake<ICollection<Shippers>>();
            A.CallTo(() => _shippersRepository.GetShippers()).Returns(shippersModel);
            A.CallTo(() => _mapper.Map<ICollection<ShipperDto>>(shippersModel)).Returns(shippers);

            // Act
            var result = _shippersController.Get();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
