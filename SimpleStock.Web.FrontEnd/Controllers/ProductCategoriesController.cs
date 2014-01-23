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
    builder.EntitySet<ProductCategory>("ProductCategories");
    builder.EntitySet<Product>("Product"); 
    builder.EntitySet<Store>("Stores"); 
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ProductCategoriesController : ODataController
    {
        private InventoryContext db = new InventoryContext();

        // GET odata/ProductCategories
        [Queryable]
        public IQueryable<ProductCategory> GetProductCategories()
        {
            return db.ProductCategories;
        }

        // GET odata/ProductCategories(5)
        [Queryable]
        public SingleResult<ProductCategory> GetProductCategory([FromODataUri] int key)
        {
            return SingleResult.Create(db.ProductCategories.Where(productcategory => productcategory.Id == key));
        }

        // PUT odata/ProductCategories(5)
        public IHttpActionResult Put([FromODataUri] int key, ProductCategory productcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != productcategory.Id)
            {
                return BadRequest();
            }

            db.Entry(productcategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductCategoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(productcategory);
        }

        // POST odata/ProductCategories
        public IHttpActionResult Post(ProductCategory productcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductCategories.Add(productcategory);
            db.SaveChanges();

            return Created(productcategory);
        }

        // PATCH odata/ProductCategories(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<ProductCategory> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ProductCategory productcategory = db.ProductCategories.Find(key);
            if (productcategory == null)
            {
                return NotFound();
            }

            patch.Patch(productcategory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductCategoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(productcategory);
        }

        // DELETE odata/ProductCategories(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            ProductCategory productcategory = db.ProductCategories.Find(key);
            if (productcategory == null)
            {
                return NotFound();
            }

            db.ProductCategories.Remove(productcategory);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/ProductCategories(5)/Products
        [Queryable]
        public IQueryable<Product> GetProducts([FromODataUri] int key)
        {
            return db.ProductCategories.Where(m => m.Id == key).SelectMany(m => m.Products);
        }

        // GET odata/ProductCategories(5)/Store
        [Queryable]
        public SingleResult<Store> GetStore([FromODataUri] int key)
        {
            return SingleResult.Create(db.ProductCategories.Where(m => m.Id == key).Select(m => m.Store));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductCategoryExists(int key)
        {
            return db.ProductCategories.Count(e => e.Id == key) > 0;
        }
    }
}
