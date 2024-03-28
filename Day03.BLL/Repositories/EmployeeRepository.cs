using Day03.BLL.Interfaces;
using Day03.DAL.Data;
using Day03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03.BLL.Repositories
{
	public class EmployeeRepository:GenericRepository<Employee> , IEmployeeRepository
	{
		

		public EmployeeRepository(ApplicationDbContext dbContext):base(dbContext)
        {
			
		}
        public IQueryable<Employee> GetEmployeeByaddress(string address)
		{
			return _DbContext.Employees.Where(E => E.Address.ToLower() == address.ToLower());
		}

		public IQueryable<Employee> SearchByname(string name)
		=>_DbContext.Employees.Where(E=>E.Name.ToLower().Contains( name));
	}
}
