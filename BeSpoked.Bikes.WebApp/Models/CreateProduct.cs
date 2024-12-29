namespace BeSpoked.Bikes.WebApp.Models
{
    public class CreateProduct
    {
        public string ProdName { get; set; }
        public string Manufacturer { get; set; }
        public string Style { get; set; }
        public double Purchase_Price { get; set; }

        public double Sale_Price { get; set; }
        public int Qty_On_Hand { get; set; }
        public int Commission_Percentage { get; set; }
    }
}
