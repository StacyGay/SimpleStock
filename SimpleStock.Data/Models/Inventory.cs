namespace SimpleStock.Data.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public double Amount { get; set; }
        public double Sold { get; set; }
        public double Lost { get; set; }
        public int ProductId { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
