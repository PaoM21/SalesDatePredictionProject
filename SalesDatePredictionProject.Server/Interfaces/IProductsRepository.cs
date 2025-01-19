using SalesDatePredictionProject.Server.Models;

namespace SalesDatePredictionProject.Server.Interfaces
{
    public interface IProductsRepository
    {
        ICollection<Products> GetProducts();
        bool ProductExists(int productId);
    }
}
