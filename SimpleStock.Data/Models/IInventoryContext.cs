using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStock.Data.Models
{
	interface IInventoryContext
	{
		DbSet<User> Users { get; set; }
		DbSet<Company> Companies { get; set; }
		DbSet<Store> Stores { get; set; }
		DbSet<Product> Products { get; set; }
		DbSet<ProductCategory> ProductCategories { get; set; }
		DbSet<Inventory> Inventories { get; set; }
	}
}
