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
    public class OrdersControllerTest
    {
        private IOrdersRepository _ordersRepository { get; set; }
        private IProductsRepository _productsRepository { get; set; }
        private IMapper _mapper { get; set; }
        private OrdersController _ordersController { get; set; }
        public OrdersControllerTest()
        {
            //Dependencies
            _mapper = A.Fake<IMapper>();
            _ordersRepository = A.Fake<IOrdersRepository>();
            _productsRepository = A.Fake<IProductsRepository>();

            //SUT
            _ordersController = new OrdersController(_ordersRepository, _productsRepository, _mapper);
        }

        [Fact]
        public void OrdersController_GetOrdersByCustom_ReturnsSuccess()
        {
            //Arrange
            var orders = A.CollectionOfFake<OrderDto>(5).ToList();
            var custId = 1;
            var ordersList = A.CollectionOfFake<Orders>(5).ToList();
            A.CallTo(() => _ordersRepository.GetOrdersByCustom(custId)).Returns(ordersList);
            A.CallTo(() => _mapper.Map<List<OrderDto>>(ordersList)).Returns(orders);

            //Act
            var result = _ordersController.Get(custId) as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(orders);
        }
    }
}