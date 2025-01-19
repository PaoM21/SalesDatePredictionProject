using SalesDatePredictionProject.Server.Dto;

namespace SalesDatePredictionProject.Server.Interfaces
{
    public interface ICustomersRepository
    {
        IEnumerable<OrderPredictionDto> GetCustomerOrderPredictions();
    }
}
