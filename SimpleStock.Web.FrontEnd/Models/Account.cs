using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SimpleStock.Data.Models;

namespace SimpleStock.Web.FrontEnd.Models
{
	public class Account
	{
		[Required]
		public Company Company { get; set; }
		[Required]
		public User User { get; set; }
		[Required]
		public IEnumerable<Store> Stores { get; set; }
	}
}