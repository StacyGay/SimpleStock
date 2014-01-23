using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStock.Core.Logging
{
	public interface ILogger
	{
		void Log(int? storeId = null, int? userId = null, string notes = "", Exception ex = null, string extraInfo = "");
	}
}
