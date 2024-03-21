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
	internal class DepartmentRepositories : IDepartmentRepositories
	{
		private readonly ApplicationDbContext _DbContext;
		public DepartmentRepositories(ApplicationDbContext dbContext) //Ask Clr for Creating Object from "ApplicationDbContext"
		{
			_DbContext = dbContext;
		}
		public int Add(Department entity)
		{
			_DbContext.Add(entity);
			return _DbContext.SaveChanges();
		}
		public int Update(Department entity)
		{
			_DbContext.Update(entity);
			return _DbContext.SaveChanges();
		}
		public int Delete(Department entity)
		{
			_DbContext.Remove(entity);
			return _DbContext.SaveChanges();
		}
		public Department Get(int id)
		{
			///var department=_DbContext.Departments.Local.Where(D=>D.Id==id).FirstOrDefault();
			///Local=>بتشوف الداتا دي رجعت قبل كده ولا لا لو رجعت بيجيبوا من اللوكال مش بيروح يجيبه من الداتابيز تاني
			///if(department == null)
			///	department=_DbContext.Departments.Local.Where(D => D.Id == id).FirstOrDefault();
			///return department;
			//==
			return _DbContext.Departments.Find(id);   //The Same Above
			//return _DbContext.Find<Department>(id);   //The Same Above
			//return _DbContext.Departments.Find(new { SrudentId=10,CourseId=1000});  
		}
		public IEnumerable<Department> GetAll()			
			=> _DbContext.Departments.AsNoTracking().ToList();
	}
}
