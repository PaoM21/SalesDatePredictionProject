namespace SalesDatePredictionProject.Server.Models
{
    public class OrderDetails
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Decimal UnitPrice { get; set; }
        public short Qty { get; set; }
        public Decimal Discount { get; set; }
    }
}
