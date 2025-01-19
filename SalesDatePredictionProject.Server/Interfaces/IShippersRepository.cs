using SalesDatePredictionProject.Server.Models;

namespace SalesDatePredictionProject.Server.Interfaces
{
    public interface IShippersRepository
    {
        ICollection<Shippers> GetShippers();
    }
}
