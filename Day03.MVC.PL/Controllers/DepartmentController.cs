using Day03.BLL.Interfaces;
using Day03.BLL.Repositories;
using Day03.DAL.Models;
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
			var departments= _DepartmentsRepo.GetAll(); 
			return View(departments);
		}

		public IActionResult Create() 
		{ 
			return View();
		}

		[HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)   //Server Side Validation
			{
               var count= _DepartmentsRepo.Add(department);
				if(count>0)
					return RedirectToAction(nameof(Index));
            }
			return View(department );
        }


        // /Department/Details/10
        // /Department/Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)       // is null
                return BadRequest();    //Helper Method from ControllerBase   //400
            var department = _DepartmentsRepo.Get(id.Value);

            if (department is null)
                return NotFound();       //404

            return View(department);
        }
    }  
}
