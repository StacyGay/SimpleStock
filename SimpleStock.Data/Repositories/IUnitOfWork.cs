using System;

namespace SimpleStock.Data.Repositories
{
	public interface IUnitOfWork : IDisposable
	{
		IRepository<T> Repository<T>() where T : class;
		void Save();
		void Dispose();
		void Dispose(bool disposing);
	}
}