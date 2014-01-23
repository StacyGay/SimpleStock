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
    builder.EntitySet<Company>("Companies");
    builder.EntitySet<Store>("Store"); 
    builder.EntitySet<User>("User"); 
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class CompaniesController : ODataController
    {
        private InventoryContext db = new InventoryContext();

        // GET odata/Companies
        [Queryable]
        public IQueryable<Company> GetCompanies()
        {
            return db.Companies;
        }

        // GET odata/Companies(5)
        [Queryable]
        public SingleResult<Company> GetCompany([FromODataUri] int key)
        {
            return SingleResult.Create(db.Companies.Where(company => company.Id == key));
        }

        // PUT odata/Companies(5)
        public IHttpActionResult Put([FromODataUri] int key, Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != company.Id)
            {
                return BadRequest();
            }

            db.Entry(company).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(company);
        }

        // POST odata/Companies
        public IHttpActionResult Post(Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Companies.Add(company);
            db.SaveChanges();

            return Created(company);
        }

        // PATCH odata/Companies(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Company> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Company company = db.Companies.Find(key);
            if (company == null)
            {
                return NotFound();
            }

            patch.Patch(company);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(company);
        }

        // DELETE odata/Companies(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Company company = db.Companies.Find(key);
            if (company == null)
            {
                return NotFound();
            }

            db.Companies.Remove(company);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/Companies(5)/Stores
        [Queryable]
        public IQueryable<Store> GetStores([FromODataUri] int key)
        {
            return db.Companies.Where(m => m.Id == key).SelectMany(m => m.Stores);
        }

        // GET odata/Companies(5)/Users
        [Queryable]
        public IQueryable<User> GetUsers([FromODataUri] int key)
        {
            return db.Companies.Where(m => m.Id == key).SelectMany(m => m.Users);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompanyExists(int key)
        {
            return db.Companies.Count(e => e.Id == key) > 0;
        }
    }
}
