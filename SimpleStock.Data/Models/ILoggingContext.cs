using System;
using System.Data.Entity;

namespace SimpleStock.Data.Models
{
	public interface ILoggingContext
	{
		DbSet<ErrorLog> ErrorLogs { get; set; }
	}
}
