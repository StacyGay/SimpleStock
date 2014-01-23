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
    builder.EntitySet<Inventory>("Inventories");
    builder.EntitySet<Product>("Products"); 
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class InventoriesController : ODataController
    {
        private InventoryContext db = new InventoryContext();

        // GET odata/Inventories
        [Queryable]
        public IQueryable<Inventory> GetInventories()
        {
            return db.Inventories;
        }

        // GET odata/Inventories(5)
        [Queryable]
        public SingleResult<Inventory> GetInventory([FromODataUri] int key)
        {
            return SingleResult.Create(db.Inventories.Where(inventory => inventory.Id == key));
        }

        // PUT odata/Inventories(5)
        public IHttpActionResult Put([FromODataUri] int key, Inventory inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != inventory.Id)
            {
                return BadRequest();
            }

            db.Entry(inventory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(inventory);
        }

        // POST odata/Inventories
        public IHttpActionResult Post(Inventory inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Inventories.Add(inventory);
            db.SaveChanges();

            return Created(inventory);
        }

        // PATCH odata/Inventories(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Inventory> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Inventory inventory = db.Inventories.Find(key);
            if (inventory == null)
            {
                return NotFound();
            }

            patch.Patch(inventory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(inventory);
        }

        // DELETE odata/Inventories(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Inventory inventory = db.Inventories.Find(key);
            if (inventory == null)
            {
                return NotFound();
            }

            db.Inventories.Remove(inventory);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/Inventories(5)/Product
        [Queryable]
        public SingleResult<Product> GetProduct([FromODataUri] int key)
        {
            return SingleResult.Create(db.Inventories.Where(m => m.Id == key).Select(m => m.Product));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InventoryExists(int key)
        {
            return db.Inventories.Count(e => e.Id == key) > 0;
        }
    }
}
