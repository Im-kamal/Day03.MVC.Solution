using Day03.BLL.Interfaces;
using Day03.BLL.Repositories;
using Day03.DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;

namespace Day03.MVC.PL.Controllers
{
	public class EmployeeController : Controller
	{

		private readonly IEmployeeRepository _employeesRepo;
		//private readonly IDepartmentRepositories _departmentRepo;
		private readonly IHostEnvironment _env;

		public EmployeeController(IEmployeeRepository employeetRepo,/*IDepartmentRepositories departmentRepo,*/ IHostEnvironment env)  //Ask Clr Create Object from "DepartmentRepositories"
		{
			_employeesRepo = employeetRepo;
			//_departmentRepo = departmentRepo;
			_env = env;
		}

		public IActionResult Index()
		{
			TempData.Keep();
			// Binding Through View's Dictionary : Transfer Data from Action To View => [One Way]
			// 
			//1.ViewData
			ViewData["Message"] = "Hello ViewData";

			//2.ViewBag
			ViewBag.Message = "Hello ViewBag";

			var employees = _employeesRepo.GetAll();
			return View(employees);
		}

		public IActionResult Create()
		{
			//ViewData["Departments"] = _departmentRepo.GetAll(); 
			return View();
		}

		[HttpPost]
		public IActionResult Create(Employee employee)
		{
			if (ModelState.IsValid)
			{
				var Count = _employeesRepo.Add(employee);

				//3.TempData
				if (Count > 0)
					TempData["Message"] = "Employee Is Created Successfully";
				else
					TempData["Message"] = "An Error ,Employee Not Created";
	
				return RedirectToAction(nameof(Index));
			}
			return View(employee);
		}

		[HttpGet]
		public IActionResult Details(int? id, string ViewName = "Details")
		{
			//ViewData["Departments"] = _departmentRepo.GetAll();

			if (!id.HasValue)
				return BadRequest();
			var employee = _employeesRepo.Get(id.Value);

			if (employee is null)
				return NotFound();
			return View(ViewName, employee);
		}

		[HttpGet]

		public IActionResult Edit(int? id)
		{
			//ViewData["Departments"] = _departmentRepo.GetAll();

			return Details(id, "Edit");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public IActionResult Edit([FromRoute] int id, Employee employee)
		{
			if (id != employee.Id)
				return BadRequest();
			if (!ModelState.IsValid)
				return View(employee);
			try
			{
				_employeesRepo.Update(employee);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				//1.Log Exeption 
				//2.Friendly Massage
				if (_env.IsDevelopment())
					ModelState.AddModelError(string.Empty, ex.Message);
				else
					ModelState.AddModelError(string.Empty, "An Error During Update The Employee");
				return View(employee);
			}
		}

		public IActionResult Delete(int? id)
		{
			//ViewData["Departments"] = _departmentRepo.GetAll();

			return Details(id, "Delete");
		}

		[HttpPost]
		public IActionResult Delete(Employee employee)
		{
			try
			{
				_employeesRepo.Delete(employee);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				if (_env.IsDevelopment())
					ModelState.AddModelError(string.Empty, ex.Message);
				else
					ModelState.AddModelError(string.Empty, "An Error During Update The Department");
				return View(employee);
			}
		}
	}
}
