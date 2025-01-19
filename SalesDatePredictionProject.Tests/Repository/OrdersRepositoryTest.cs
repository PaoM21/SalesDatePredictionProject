using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SalesDatePredictionProject.Server.Data;
using SalesDatePredictionProject.Server.Models;
using SalesDatePredictionProject.Server.Repository;

namespace SalesDatePredictionProject.Tests.Repository
{
    public class OrdersRepositoryTest
    {
        private async Task<DataContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();

            if (await databaseContext.Orders.CountAsync() < default(int))
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Orders.Add(
                        new Orders()
                        {
                            OrderId = 1,
                            CustId = 1,
                            EmpId = 1,
                            OrderDate = DateTime.Now,
                            RequiredDate = DateTime.Now,
                            ShippedDate = DateTime.Now,
                            ShipperId = 1,
                            Freight = 10,
                            ShipName = "Name",
                            ShipAddress = "Address",
                            ShipCity = "City",
                            ShipRegion = "Region",
                            ShipPostalCode = "PostalCode",
                            ShipCountry = "Country"
                        });

                    await databaseContext.SaveChangesAsync();
                }
            }

            return databaseContext;
        }

        [Fact]
        public async void OrdersRepository_GetOrdersByCustom_ReturnsICollection()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var ordersRepository = new OrdersRepository(dbContext);
            int custId = 1;

            //Act
            var result = ordersRepository.GetOrdersByCustom(custId);

            //Assert
            result.Should().NotBeNull();
            result.Should().AllBeOfType<Orders>();
            result.Should().BeOfType<ICollection<Orders>>();
        }

        [Fact]
        public async void OrdersRepository_GetOrders_ReturnsICollection()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var ordersRepository = new OrdersRepository(dbContext);

            //Act
            var result = ordersRepository.GetOrders();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ICollection<Orders>>();
        }

        [Fact]
        public async void OrdersRepository_CustomerExists_ReturnsBool()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var ordersRepository = new OrdersRepository(dbContext);
            int custId = 1;

            //Act
            var result = ordersRepository.CustomerExists(custId);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void OrdersRepository_OrderExists_ReturnsBool()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var ordersRepository = new OrdersRepository(dbContext);
            int orerId = 1;

            //Act
            var result = ordersRepository.OrderExists(orerId);

            //Assert
            result.Should().BeTrue();
        }
    }
}
