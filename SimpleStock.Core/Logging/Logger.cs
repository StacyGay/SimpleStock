using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using SimpleStock.Data.Models;

namespace SimpleStock.Core.Logging
{
	public class Logger: ILogger
	{
		private readonly InventoryContext _context;

		public Logger(InventoryContext context)
		{
			_context = context;
		}

		public void Log(int? storeId = null, int? userId = null, string notes = "", Exception ex = null, string extraInfo = "")
		{
			var errorLog = new ErrorLog
			{
				StoreId = storeId,
				UserId = userId,
				Notes = notes,
				ExtraInfo = extraInfo
			};
			if (ex != null)
				errorLog.Exception = ex.ToString();

			_context.ErrorLogs.Add(errorLog);
			_context.SaveChanges();
		}
	}
}
