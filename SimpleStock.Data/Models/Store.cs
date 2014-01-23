using System.Collections.Generic;
namespace SimpleStock.Data.Models
{
    public class Store
    {
        public Store()
        {
            Users = new HashSet<User>();
            Products = new HashSet<Product>();
            ProductCategories = new HashSet<ProductCategory>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
