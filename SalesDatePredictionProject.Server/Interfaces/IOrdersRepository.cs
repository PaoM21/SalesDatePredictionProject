using SalesDatePredictionProject.Server.Dto;
using SalesDatePredictionProject.Server.Models;
using System.Net;

namespace SalesDatePredictionProject.Server.Interfaces
{
    public interface IOrdersRepository
    {
        ICollection<Orders> GetOrdersByCustom(int custId);
        int CreateOrderWithProduct(OrderProductDto orderProduct);
        bool OrderExists(int orderId);
        bool CustomerExists(int custId);
    }
}
