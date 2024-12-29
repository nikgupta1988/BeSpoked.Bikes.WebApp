namespace BeSpoked.Bikes.WebApp.Models
{
    public class SaleModel
    {
        public DateTime sellDate { get; set; }

        public Guid ProductsProductID { get; set; }

        public Guid SalesPersonSP_ID { get; set; }
        public Guid CustomerCUST_ID { get; set; }
    }
}
