using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SalesDatePredictionProject.Server.Data;
using SalesDatePredictionProject.Server.Models;
using SalesDatePredictionProject.Server.Repository;

namespace SalesDatePredictionProject.Tests.Repository
{
    public class EmployeesRepositoryTest
    {
        private async Task<DataContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();

            if (await databaseContext.Employees.CountAsync() == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Employees.Add(
                        new Employees()
                        {
                            EmpId = i + 1,
                            LastName = "Perez",
                            FirstName = "Pepita",
                            Title = "Title",
                            TitleOfCourtesy = "",
                            BirthDate = DateTime.Now,
                            HireDate = DateTime.Now,
                            Address = "cra 1 # 1 - 1",
                            City = "Bogotá",
                            Region = "Colombia",
                            PostalCode = "111111",
                            Country = "Colombia",
                            Phone = "3003003003",
                            Mgrid = 1
                        });

                    await databaseContext.SaveChangesAsync();
                }
            }

            return databaseContext;
        }

        [Fact]
        public async Task EmployeesRepository_GetEmployees_ReturnsICollection()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var employeesRepository = new EmployeesRepository(dbContext);

            //Act
            var result = employeesRepository.GetEmployees();

            //Assert
            result.Should().NotBeNull();
            result.Should().AllBeOfType<Employees>();
            result.Should().BeAssignableTo<ICollection<Employees>>();
        }
    }
}
