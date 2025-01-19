using Microsoft.EntityFrameworkCore;
using SalesDatePredictionProject.Server.Data;
using SalesDatePredictionProject.Server.Dto;
using SalesDatePredictionProject.Server.Interfaces;

namespace SalesDatePredictionProject.Server.Repository
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly DataContext _context;
        public CustomersRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<OrderPredictionDto> GetCustomerOrderPredictions()
        {
            var query = @"
                WITH CTE0 AS (
                    SELECT MAX(orderdate) AS LastOrderDate, custid, orderdate
                    FROM Sales.Orders
                    GROUP BY custid, orderdate
                ), 
                CTE1 AS (
                    SELECT
                        custid,
                        LastOrderDate,
                        LAG(orderdate) OVER (PARTITION BY custid ORDER BY orderdate) AS FechaAnterior,
                        ROW_NUMBER() OVER (PARTITION BY custid ORDER BY LastOrderDate DESC) AS RN
                    FROM CTE0
                ), 
                CTE2 AS (
                    SELECT
                        c.custid,
                        AVG(DATEDIFF(DAY, c.FechaAnterior, c.LastOrderDate)) AS PromedioDias
                    FROM CTE1 c
                    WHERE c.FechaAnterior IS NOT NULL
                    GROUP BY c.custid
                )
                SELECT 
                    Cu.companyname,
                    c1.LastOrderDate,
                    DATEADD(DAY, c2.PromedioDias, c1.LastOrderDate) AS NextPredictedOrder
                FROM CTE1 c1
                JOIN CTE2 c2 ON c1.custid = c2.custid
                JOIN Sales.Customers AS Cu ON c1.custid = Cu.custid
                WHERE c1.RN = 1
                ORDER BY companyname;";

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    var predictions = new List<OrderPredictionDto>();
                    while (result.Read())
                    {
                        predictions.Add(new OrderPredictionDto
                        {
                            CompanyName = result.GetString(0),
                            LastOrderDate = result.GetDateTime(1),
                            NextPredictedOrder = result.GetDateTime(2)
                        });
                    }
                    return predictions;
                }
            }
        }
    }
}
