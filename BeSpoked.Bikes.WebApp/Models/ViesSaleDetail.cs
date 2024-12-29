namespace BeSpoked.Bikes.WebApp.Models
{
    public class ViesSaleDetail
    {
        public string saleID { get; set; }
        public DateTime sellDate { get; set; }
        public string productsProductID { get; set; }
        public string salesPersonSP_ID { get; set; }
        public string customerCUST_ID { get; set; }
        public Products products { get; set; }
        public SalesPerson salesPerson { get; set; }
        public Customer customer { get; set; }

    }
    public class SalesPerson
    {
        public string sP_ID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public DateTime startDate { get; set; }
        public DateTime terminationdate { get; set; }
        public string manager { get; set; }
    }
    public class Customer
    {
        public string cusT_ID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public DateTime startDate { get; set; }
    }

    public class Products
    {
        public string productID { get; set; }
        public string prodName { get; set; }
        public string manufacturer { get; set; }
        public string style { get; set; }
        public int purchase_Price { get; set; }
        public int sale_Price { get; set; }
        public int qty_On_Hand { get; set; }
        public int commission_Percentage { get; set; }
    }

}
