using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleStock.Data.Models;

namespace SimpleStock.Test.Manual
{
	class Program
	{
		static void Main(string[] args)
		{
			var db = new InventoryContext();

			var company = new Company()
			{
				Name = "Gay Enterprises",
				Address = "123 Test ln",
				City = "Testville",
				State = "SC",
				PostalCode = "29577"
			};

			db.Companies.Add(company);

			db.SaveChanges();

			var user = new User()
			{
				Company = company,
				Email = "stacygay@gmail.com",
				CompanyId = company.Id,
				FirstName = "Stacy",
				LastName = "Gay",
				Password = "unlight"
			};

			db.Users.Add(user);

			db.SaveChanges();
		}
	}
}
