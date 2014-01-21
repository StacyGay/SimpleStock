using System.Collections.Generic;

namespace SimpleStock.Data.Repositories
{
	public class PagedResult<T>
	{
		private readonly IEnumerable<T> _items;
		private readonly int _totalCount;

		public PagedResult(IEnumerable<T> items, int totalCount)
		{
			_items = items;
			_totalCount = totalCount;
		}

		public IEnumerable<T> Items { get { return _items; } }
		public int TotalCount { get { return _totalCount; } }
	}
}
