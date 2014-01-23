using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStock.Data.Models
{
	public class ErrorLog
	{
		public ErrorLog()
		{
			TimeStamp = DateTime.Now;
		}

		public long Id { get; set; }
		public int? StoreId { get; set; }
		public int? UserId { get; set; }
		public string Notes { get; set; }
		public string Exception { get; set; }
		public string ExtraInfo { get; set; }
		public DateTime TimeStamp { get; set; }
	}
}
