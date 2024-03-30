using Day03.BLL.Interfaces;
using Day03.BLL.Repositories;
using Day03.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03.BLL
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _dbContext;

		public IEmployeeRepository EmployeeRepository { get ; set ; }
		public IDepartmentRepositories DepartmentRepository { get ; set ; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
		    _dbContext = dbContext;
			EmployeeRepository =new EmployeeRepository(_dbContext);
			DepartmentRepository= new DepartmentRepositories(_dbContext);
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
