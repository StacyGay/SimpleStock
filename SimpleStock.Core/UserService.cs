using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleStock.Data.Models;
using SimpleStock.Data.Repositories;

namespace SimpleStock.Core
{
	public class UserService
	{
		public UserService()
		{
			
		}

		public User GetUser(string email, string password)
		{
			return new User();
		}
	}
}
