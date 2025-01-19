using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SalesDatePredictionProject.Server.Data;
using SalesDatePredictionProject.Server.Models;
using SalesDatePredictionProject.Server.Repository;

namespace SalesDatePredictionProject.Tests.Repository
{
    public class ProductsRepositoryTest
    {
        private async Task<DataContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();

            if (await databaseContext.Products.CountAsync() == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Products.Add(
                        new Products()
                        {
                            ProductId = i + 1,
                            ProductName = $"Product {i + 1}",
                            SupplierId = 1,
                            CategoryId = 1,
                            UnitPrice = 1000,
                            Discontinued = true
                        });
                }

                await databaseContext.SaveChangesAsync();
            }

            return databaseContext;
        }

        [Fact]
        public async Task ProductsRepository_GetProductsByCustom_ReturnsICollection()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var productsRepository = new ProductsRepository(dbContext);

            //Act
            var result = productsRepository.GetProducts();

            //Assert
            result.Should().NotBeNull();
            result.Should().AllBeOfType<Products>();
            result.Should().BeAssignableTo<ICollection<Products>>();
        }

        [Fact]
        public async Task ProductsRepository_ProductExists_ReturnsBool()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var productsRepository = new ProductsRepository(dbContext);
            int productId = 1;

            //Act
            var result = productsRepository.ProductExists(productId);

            //Assert
            result.Should().BeTrue();
        }
    }
}
