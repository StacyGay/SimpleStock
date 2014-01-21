using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStock.Data.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly DbContext _context;

		private bool _disposed;
		private Hashtable _repositories;

		public UnitOfWork(DbContext context)
		{
			_context = context;
		}

		public IRepository<T> Repository<T>() where T : class
		{
			if (_repositories == null)
				_repositories = new Hashtable();

			var type = typeof(T).Name;

			if (!_repositories.ContainsKey(type))
			{
				var repositoryType = typeof(Repository<>);

				var repositoryInstance =
					Activator.CreateInstance(repositoryType
							.MakeGenericType(typeof(T)), _context);

				_repositories.Add(type, repositoryInstance);
			}

			return (IRepository<T>)_repositories[type];
		}

		public void Save()
		{
			_context.SaveChanges();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public virtual void Dispose(bool disposing)
		{
			if (!_disposed)
				if (disposing)
					_context.Dispose();

			_disposed = true;
		}
	}
}
