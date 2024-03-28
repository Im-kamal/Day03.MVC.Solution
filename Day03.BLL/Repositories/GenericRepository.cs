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
	public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
	{
		private protected readonly ApplicationDbContext _DbContext;
		public GenericRepository(ApplicationDbContext dbContext) //Ask Clr for Creating Object from "ApplicationDbContext"
		{
			_DbContext = dbContext;
		}
		public int Add(T entity)
		{
			_DbContext.Set<T>().Add(entity);
			return _DbContext.SaveChanges();
		}
		public int Update(T entity)
		{
			_DbContext.Set<T>().Update(entity);
			return _DbContext.SaveChanges();
		}
		public int Delete(T entity)
		{
			_DbContext.Set<T>().Remove(entity);
			return _DbContext.SaveChanges();
		}
		public T Get(int id)
		{
			///var department=_DbContext.Departments.Local.Where(D=>D.Id==id).FirstOrDefault();
			///Local=>بتشوف الداتا دي رجعت قبل كده ولا لا لو رجعت بيجيبوا من اللوكال مش بيروح يجيبه من الداتابيز تاني
			///if(department == null)
			///	department=_DbContext.Set<T>().Local.Where(D => D.Id == id).FirstOrDefault();
			///return department;
			//==
			return _DbContext.Find<T>(id);   //The Same Above
											 //return _DbContext.Find<Department>(id);   //The Same Above
											 //return _DbContext.Departments.Find(new { SrudentId=10,CourseId=1000});  
		}
		public IEnumerable<T> GetAll()
		{
			if (typeof(T) == typeof(Employee))
				return (IEnumerable<T>)_DbContext.Employees.Include(E => E.Department).AsNoTracking().ToList();
			else
				return _DbContext.Set<T>().AsNoTracking().ToList();


		}
	}
}
