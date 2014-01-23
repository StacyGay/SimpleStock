using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleStock.Data.Models
{
    public class Product
    {
        public Product()
        {
            Inventories = new HashSet<Inventory>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Units { get; set; }
        public decimal Cost { get; set; }
        public string Price { get; set; }
        public int StoreId { get; set; }
		
        public int? ProductCategoryId { get; set; }
    
        public virtual Store Store { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
    }
}
