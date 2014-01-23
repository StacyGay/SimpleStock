using System.Collections.Generic;
namespace SimpleStock.Data.Models
{
    public class ProductCategory
    {
        public ProductCategory()
        {
            Products = new HashSet<Product>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int StoreId { get; set; }
    
        public virtual Store Store { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
