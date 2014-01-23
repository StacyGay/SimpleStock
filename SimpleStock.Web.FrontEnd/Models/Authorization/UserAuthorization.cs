using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleStock.Data.Models;

namespace SimpleStock.Web.FrontEnd.Models.Authorization
{
	public class UserAuthorization : IBasicAuthMethod
	{
		public bool Authorize(string username, string password)
		{
			using (var context = new InventoryContext())
			{
				var user = context.Users.FirstOrDefault(u => u.Email == username);
				if (user == null)
					return false;
				if (!user.IsPasswordValid(password))
					return false;
			}

			return true;
		}
	}
}