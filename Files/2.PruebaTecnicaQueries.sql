--Sales Date Prediction
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
ORDER BY CustomerName

--Get Client Orders
SELECT [orderid]
      ,[requireddate]
      ,[shippeddate]
      ,[shipname]
      ,[shipaddress]
      ,[shipcity]
FROM [StoreSample].[Sales].[Orders]

--Get employees
SELECT [empid]
	   ,CONCAT([firstname], [lastname]) AS FullName
FROM [StoreSample].[HR].[Employees]

--Get Shippers
SELECT [shipperid]
      ,[companyname]
FROM [StoreSample].[Sales].[Shippers]

--Get Products
SELECT [productid]
      ,[productname]
FROM [StoreSample].[Production].[Products]

--Add New Order
CREATE ALTER PROCEDURE AddNewOrder
    @Empid INT,
    @Shipperid INT,
    @Shipname NVARCHAR(100),
    @Shipaddress NVARCHAR(255),
    @Shipcity NVARCHAR(100),
    @Orderdate DATETIME,
    @Requireddate DATETIME,
    @Shippeddate DATETIME,
    @Freight DECIMAL(18, 2),
    @Shipcountry NVARCHAR(100),
    @Productid INT,
    @Unitprice DECIMAL(18, 2),
    @Qty INT,
    @Discount DECIMAL(5, 2)
AS
BEGIN
    DECLARE @Orderid INT;
    BEGIN TRANSACTION;
    BEGIN TRY
        INSERT INTO [Sales].[Orders] ([empid], [shipperid], [shipname], [shipaddress], [shipcity], [orderdate], [requireddate], [shippeddate], [freight], [shipcountry])
        VALUES (@Empid, @Shipperid, @Shipname, @Shipaddress, @Shipcity, @Orderdate, @Requireddate, @Shippeddate, @Freight, @Shipcountry);

        SET @Orderid = SCOPE_IDENTITY();

		INSERT INTO [Sales].[OrderDetails] ([orderid], [productid], [unitprice], [qty], [discount])
		VALUES (@Orderid, @Productid, @Unitprice, @Qty, @Discount)

        COMMIT TRANSACTION;

        SELECT @Orderid AS OrderId;

    END TRY
    BEGIN CATCH
        -- Revertir la transacción en caso de error
        ROLLBACK TRANSACTION;

        -- Manejo de errores
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
