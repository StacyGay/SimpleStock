namespace SimpleStock.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddErrorLog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        PostalCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CompanyId = c.Int(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.CompanyId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StoreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Units = c.String(),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.String(),
                        StoreId = c.Int(nullable: false),
                        ProductCategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategoryId)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.ProductCategoryId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        Sold = c.Double(nullable: false),
                        Lost = c.Double(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CompanyId = c.Int(nullable: false),
                        Salt = c.Binary(nullable: false),
                        PasswordHash = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.ErrorLogs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        StoreId = c.Int(),
                        UserId = c.Int(),
                        Notes = c.String(),
                        Exception = c.String(),
                        ExtraInfo = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StoreUsers",
                c => new
                    {
                        StoreId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StoreId, t.UserId })
                .ForeignKey("dbo.Stores", t => t.StoreId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.StoreId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StoreUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.StoreUsers", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.Stores", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.ProductCategories", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.Products", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.Inventories", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Stores", "CompanyId", "dbo.Companies");
            DropIndex("dbo.StoreUsers", new[] { "UserId" });
            DropIndex("dbo.StoreUsers", new[] { "StoreId" });
            DropIndex("dbo.Stores", new[] { "User_Id" });
            DropIndex("dbo.Users", new[] { "CompanyId" });
            DropIndex("dbo.ProductCategories", new[] { "StoreId" });
            DropIndex("dbo.Products", new[] { "StoreId" });
            DropIndex("dbo.Products", new[] { "ProductCategoryId" });
            DropIndex("dbo.Inventories", new[] { "ProductId" });
            DropIndex("dbo.Stores", new[] { "CompanyId" });
            DropTable("dbo.StoreUsers");
            DropTable("dbo.ErrorLogs");
            DropTable("dbo.Users");
            DropTable("dbo.Inventories");
            DropTable("dbo.Products");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Stores");
            DropTable("dbo.Companies");
        }
    }
}
