using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using SimpleStock.Data.Models;

namespace SimpleStock.Web.FrontEnd.Controllers
{
    /*
    To add a route for this controller, merge these statements into the Register method of the WebApiConfig class. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using SimpleStock.Data.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Product>("Products");
    builder.EntitySet<Inventory>("Inventory"); 
    builder.EntitySet<ProductCategory>("ProductCategories"); 
    builder.EntitySet<Store>("Stores"); 
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ProductsController : ODataController
    {
        private InventoryContext db = new InventoryContext();

        // GET odata/Products
        [Queryable]
        public IQueryable<Product> GetProducts()
        {
            return db.Products;
        }

        // GET odata/Products(5)
        [Queryable]
        public SingleResult<Product> GetProduct([FromODataUri] int key)
        {
            return SingleResult.Create(db.Products.Where(product => product.Id == key));
        }

        // PUT odata/Products(5)
        public IHttpActionResult Put([FromODataUri] int key, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != product.Id)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(product);
        }

        // POST odata/Products
        public IHttpActionResult Post(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            db.SaveChanges();

            return Created(product);
        }

        // PATCH odata/Products(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Product> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product product = db.Products.Find(key);
            if (product == null)
            {
                return NotFound();
            }

            patch.Patch(product);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(product);
        }

        // DELETE odata/Products(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Product product = db.Products.Find(key);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/Products(5)/Inventories
        [Queryable]
        public IQueryable<Inventory> GetInventories([FromODataUri] int key)
        {
            return db.Products.Where(m => m.Id == key).SelectMany(m => m.Inventories);
        }

        // GET odata/Products(5)/ProductCategory
        [Queryable]
        public SingleResult<ProductCategory> GetProductCategory([FromODataUri] int key)
        {
            return SingleResult.Create(db.Products.Where(m => m.Id == key).Select(m => m.ProductCategory));
        }

        // GET odata/Products(5)/Store
        [Queryable]
        public SingleResult<Store> GetStore([FromODataUri] int key)
        {
            return SingleResult.Create(db.Products.Where(m => m.Id == key).Select(m => m.Store));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int key)
        {
            return db.Products.Count(e => e.Id == key) > 0;
        }
    }
}
