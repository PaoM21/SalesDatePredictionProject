using SalesDatePredictionProject.Server.Data;
using SalesDatePredictionProject.Server.Interfaces;
using SalesDatePredictionProject.Server.Models;

namespace SalesDatePredictionProject.Server.Repository
{
    public class EmployeesRepository : IEmployeesRepository
    {

        private readonly DataContext _context;
        public EmployeesRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Employees> GetEmployees()
        {
            return _context.Employees.OrderBy(or => or.EmpId).ToList();
        }
    }
}
