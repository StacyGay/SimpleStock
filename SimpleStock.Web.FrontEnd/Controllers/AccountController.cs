using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SimpleStock.Data.Models;
using SimpleStock.Web.FrontEnd.Models;
using WebGrease.Css.Extensions;

namespace SimpleStock.Web.FrontEnd.Controllers
{
    public class AccountController : ApiController
    {
        private InventoryContext db = new InventoryContext();

        // GET api/Account/5
        [ResponseType(typeof(Account))]
        public IHttpActionResult GetAccount(LoginRequest login)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == login.Email);
            if (user == null || !user.IsPasswordValid(login.Password))
                return NotFound();

	        var account = new Account
	        {
		        User = user,
		        Company = user.Company,
		        Stores = user.Stores
	        };

            return Ok(account);
        }

        // PUT api/Account/5
        public IHttpActionResult PutAccount(Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(account.User).State = EntityState.Modified;
			db.Entry(account.Company).State = EntityState.Modified;
			account.Stores.ForEach(s => db.Entry(s).State = EntityState.Modified);
	        db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Account
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

	        db.Companies.Add(account.Company);
			db.SaveChanges();
	        account.User.CompanyId = account.Company.Id;
            db.Users.Add(account.User);
			db.SaveChanges();
            account.Stores.ToList().ForEach(s =>
            {
	            s.CompanyId = account.Company.Id;
				s.Users = new List<User> { account.User };
            });
	        db.Stores.AddRange(account.Stores);
			db.SaveChanges();

            return CreatedAtRoute("DefaultApi", null, account);
        }

        /*// DELETE api/Account/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }*/

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}