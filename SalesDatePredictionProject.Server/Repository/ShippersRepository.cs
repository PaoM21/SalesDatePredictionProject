using SalesDatePredictionProject.Server.Data;
using SalesDatePredictionProject.Server.Interfaces;
using SalesDatePredictionProject.Server.Models;

namespace SalesDatePredictionProject.Server.Repository
{
    public class ShippersRepository : IShippersRepository
    {
        private readonly DataContext _context;
        public ShippersRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Shippers> GetShippers()
        {
            return _context.Shippers.OrderBy(sh => sh.ShipperId).ToList();
        }
    }
}
