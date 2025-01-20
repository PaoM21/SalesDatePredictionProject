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
                    SELECT
	                custid, 
	                orderdate,
	                DATEDIFF(DAY, LAG(orderdate) OVER (PARTITION BY custid ORDER BY orderdate),  orderdate) AS DiasEntreOrdenes
                    FROM Sales.Orders
                ),
                CTE1 AS (
	                SELECT AVG(DiasEntreOrdenes) AS DiasPromedio,
		                custid
	                FROM CTE0
	                WHERE DiasEntreOrdenes IS NOT NULL
	                GROUP BY custid
                ),
                CTE2 AS (
	                SELECT orderdate, 
	                custid
	                FROM 
		                (SELECT ROW_NUMBER() OVER (PARTITION BY custid ORDER BY orderdate DESC) AS RN,
			                custid,
			                orderdate
		                FROM Sales.Orders) AS OrderByCustom
	                WHERE RN = 1
                )
                SELECT CTE2.custid,
	                Cu.companyname AS CustomerName,
	                orderdate AS LastOrderDate,
	                DATEADD(DAY, DiasPromedio, orderdate) AS NextPredictedOrder
                FROM CTE2
                INNER JOIN Sales.Customers AS Cu 
	                ON Cu.custid = CTE2.custid
                INNER JOIN CTE1 
	                ON CTE1.custid = CTE2.custid
                ORDER BY CustomerName";

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
                            CustId = result.GetInt32(0),
                            CompanyName = result.GetString(1),
                            LastOrderDate = result.GetDateTime(2),
                            NextPredictedOrder = result.GetDateTime(3)
                        });
                    }
                    return predictions;
                }
            }
        }
    }
}
