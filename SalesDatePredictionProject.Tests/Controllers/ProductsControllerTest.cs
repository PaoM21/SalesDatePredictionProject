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
    public class ProductsControllerTest
    {
        private IProductsRepository _productsRepository { get; set; }
        private IMapper _mapper { get; set; }
        private ProductsController _productsController { get; set; }
        public ProductsControllerTest()
        {
            //Dependencies
            _mapper = A.Fake<IMapper>();
            _productsRepository = A.Fake<IProductsRepository>();

            //SUT
            _productsController = new ProductsController(_productsRepository, _mapper);
        }

        [Fact]
        public void ProductsController_GetProducts_ReturnsSuccess()
        {
            // Arrange
            var products = A.CollectionOfFake<Products>(5);
            var productsDto = A.CollectionOfFake<ProductDto>(5);
            A.CallTo(() => _productsRepository.GetProducts()).Returns(products);
            A.CallTo(() => _mapper.Map<IEnumerable<ProductDto>>(products)).Returns(productsDto);

            // Act
            var result = _productsController.Get();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(productsDto);
        }
    }
}
