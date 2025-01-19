using SalesDatePredictionProject.Server.Models;

namespace SalesDatePredictionProject.Server.Interfaces
{
    public interface IEmployeesRepository
    {
        ICollection<Employees> GetEmployees();
    }
}
