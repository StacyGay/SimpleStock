using System;
using System.Data.Entity;

namespace SimpleStock.Data.Repositories
{
	public interface IUnitOfWork<TContext> : IDisposable
		where TContext : DbContext
	{
		IRepository<TEntity> Repository<TEntity>() where TEntity : class;
		void Save();
		new void Dispose();
		void Dispose(bool disposing);
	}
}