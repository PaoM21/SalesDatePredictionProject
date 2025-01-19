using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SalesDatePredictionProject.Server.Data;
using SalesDatePredictionProject.Server.Models;
using SalesDatePredictionProject.Server.Repository;

namespace SalesDatePredictionProject.Tests.Repository
{
    public class ShippersRepositoryTest
    {
        private async Task<DataContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();

            if (await databaseContext.Shippers.CountAsync() == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Shippers.Add(
                        new Shippers()
                        {
                            ShipperId = i + 1,
                            CompanyName = $"Name {i + 1}",
                            Phone = $"300300300{i + 1}"
                        });
                }

                await databaseContext.SaveChangesAsync();
            }

            return databaseContext;
        }

        [Fact]
        public async Task ShippersRepository_GetShippersByCustom_ReturnsICollection()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var shippersRepository = new ShippersRepository(dbContext);

            //Act
            var result = shippersRepository.GetShippers();

            //Assert
            result.Should().NotBeNull();
            result.Should().AllBeOfType<Shippers>();
            result.Should().BeAssignableTo<ICollection<Shippers>>();
        }
    }
}
