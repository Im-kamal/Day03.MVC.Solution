using Day03.BLL.Interfaces;
using Day03.BLL.Repositories;
using Day03.DAL.Data;
using Day03.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03.BLL
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _dbContext;

		//private Dictionary<string, IGenericRepository<ModelBase>> _reposetories;
		private Hashtable _reposetories;

		public UnitOfWork(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public IGenericRepository<T> Repository<T>() where T : ModelBase
		{
			var Key = typeof(T).Name;
			if (!_reposetories.ContainsKey(Key))
			{
				if (Key == nameof(Employee))
				{
					var repository = new EmployeeRepository(_dbContext);
					_reposetories.Add(Key, repository);
				}
				else
				{
					var repository = new GenericRepository<T>(_dbContext);

					_reposetories.Add(Key, repository);
				}
			}
			return _reposetories[Key] as IGenericRepository<T>;
		}
		public int Complete()
		{
			return _dbContext.SaveChanges();
		}
		public void Dispose()
		{
			_dbContext.Dispose();
		}


	}
}
