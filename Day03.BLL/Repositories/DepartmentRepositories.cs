using Day03.BLL.Interfaces;
using Day03.DAL.Data;
using Day03.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03.BLL.Repositories
{
	public class DepartmentRepositories:GenericRepository<Department> , IDepartmentRepositories
	{
		public DepartmentRepositories(ApplicationDbContext dbContext) : base(dbContext)
		{

		}
	}
}
