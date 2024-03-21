using Day03.BLL.Interfaces;
using Day03.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Day03.MVC.PL.Controllers
{
	//Inheritance: DepartmentController is a Controller
	//Composition: DepartmentController is a DepartmentRepositories
	public class DepartmentController : Controller
	{
		private readonly IDepartmentRepositories _DepartmentsRepo;

        public DepartmentController(IDepartmentRepositories departmentRepo)  //Ask Clr Create Object from "DepartmentRepositories"
		{
			_DepartmentsRepo= departmentRepo;

		}
        public IActionResult Index()
		{
			return View();
		}
	}  
}
