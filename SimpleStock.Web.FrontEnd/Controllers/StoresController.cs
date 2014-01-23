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
    builder.EntitySet<Store>("Stores");
    builder.EntitySet<Company>("Companies"); 
    builder.EntitySet<ProductCategory>("ProductCategory"); 
    builder.EntitySet<Product>("Product"); 
    builder.EntitySet<User>("User"); 
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class StoresController : ODataController
    {
        private InventoryContext db = new InventoryContext();

        // GET odata/Stores
        [Queryable]
        public IQueryable<Store> GetStores()
        {
            return db.Stores;
        }

        // GET odata/Stores(5)
        [Queryable]
        public SingleResult<Store> GetStore([FromODataUri] int key)
        {
            return SingleResult.Create(db.Stores.Where(store => store.Id == key));
        }

        // PUT odata/Stores(5)
        public IHttpActionResult Put([FromODataUri] int key, Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != store.Id)
            {
                return BadRequest();
            }

            db.Entry(store).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(store);
        }

        // POST odata/Stores
        public IHttpActionResult Post(Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Stores.Add(store);
            db.SaveChanges();

            return Created(store);
        }

        // PATCH odata/Stores(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Store> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Store store = db.Stores.Find(key);
            if (store == null)
            {
                return NotFound();
            }

            patch.Patch(store);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(store);
        }

        // DELETE odata/Stores(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Store store = db.Stores.Find(key);
            if (store == null)
            {
                return NotFound();
            }

            db.Stores.Remove(store);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/Stores(5)/Company
        [Queryable]
        public SingleResult<Company> GetCompany([FromODataUri] int key)
        {
            return SingleResult.Create(db.Stores.Where(m => m.Id == key).Select(m => m.Company));
        }

        // GET odata/Stores(5)/ProductCategories
        [Queryable]
        public IQueryable<ProductCategory> GetProductCategories([FromODataUri] int key)
        {
            return db.Stores.Where(m => m.Id == key).SelectMany(m => m.ProductCategories);
        }

        // GET odata/Stores(5)/Products
        [Queryable]
        public IQueryable<Product> GetProducts([FromODataUri] int key)
        {
            return db.Stores.Where(m => m.Id == key).SelectMany(m => m.Products);
        }

        // GET odata/Stores(5)/Users
        [Queryable]
        public IQueryable<User> GetUsers([FromODataUri] int key)
        {
            return db.Stores.Where(m => m.Id == key).SelectMany(m => m.Users);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StoreExists(int key)
        {
            return db.Stores.Count(e => e.Id == key) > 0;
        }
    }
}
