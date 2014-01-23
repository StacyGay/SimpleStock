using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SimpleStock.Data.Models
{
	public class InventoryContext : DbContext, IInventoryContext, ILoggingContext
	{
		public InventoryContext()
			: base("name=InventoryContext")
		{

		}

		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<Company> Companies { get; set; }
		public virtual DbSet<Store> Stores { get; set; }
		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<ProductCategory> ProductCategories { get; set; }
		public virtual DbSet<Inventory> Inventories { get; set; }
		public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{

			//modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
			modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

			modelBuilder.Entity<Store>()
				.HasMany(s => s.Users)
				.WithMany()
				.Map(m =>
				{
					m.MapLeftKey("StoreId");
					m.MapRightKey("UserId");
					m.ToTable("StoreUsers");
				});

			/*modelBuilder.Entity<Product>()
				.HasRequired(p => p.Store)
				.WithRequiredDependent()
				.WillCascadeOnDelete(false);*/

			/*modelBuilder.Entity<Product>()
				.HasRequired(p => p.ProductCategory)
				.WithRequiredDependent()
				.WillCascadeOnDelete(true);*/
		}
	}

}
