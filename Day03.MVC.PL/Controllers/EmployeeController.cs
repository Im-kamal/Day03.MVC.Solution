﻿using AutoMapper;
using Castle.Core.Internal;
using Day03.BLL.Interfaces;
using Day03.BLL.Repositories;
using Day03.DAL.Models;
using Day03.MVC.PL.Helpers;
using Day03.MVC.PL.ViweModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVCProject.PL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Day03.MVC.PL.Controllers
{
	[Authorize]

	public class EmployeeController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		//private readonly IEmployeeRepository _employeesRepo;
		//private readonly IDepartmentRepositories _departmentRepo;
		private readonly IHostEnvironment _env;

		public EmployeeController(
			IUnitOfWork unitOfWork,
			//IEmployeeRepository employeetRepo,
			/*IDepartmentRepositories departmentRepo,*/
			IMapper mapper,
			IHostEnvironment env)
		//Ask Clr Create Object from "DepartmentRepositories"
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_env = env;
		}

		public async Task<IActionResult> Index(string searchInp)
		{
			TempData.Keep();
			// Binding Through View's Dictionary : Transfer Data from Action To View => [One Way]
			// 
			////1.ViewData
			//ViewData["Message"] = "Hello ViewData";
			//
			////2.ViewBag
			//ViewBag.Message = "Hello ViewBag";

			var employees = Enumerable.Empty<Employee>();
			var employeeRepo = _unitOfWork.Repository<Employee>() as EmployeeRepository;
			if (string.IsNullOrEmpty(searchInp))
				employees = await employeeRepo.GetAllAsync();
			else
				employees = employeeRepo.SearchByname(searchInp);

			return View(_mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees));

		}

		public IActionResult Create()
		{
			//ViewData["Departments"] = _departmentRepo.GetAll(); 
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
		{
			if (ModelState.IsValid)
			{
				employeeVM.ImageName = await DocumentSettings.UploadFile(employeeVM.Image, "Images");

				var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

				_unitOfWork.Repository<Employee>().Add(MappedEmp);

				//2.Update Department
				//_unitOfWork..Repository<Department >().Update(department)
				//3.Delete Department
				//_unitOfWork..Repository<Project>().Delete(department)

				//_dbContext.SaveChanges();
				var Count = await _unitOfWork.Complete();
				//3.TempData
				if (Count > 0)
				{
					TempData["Message"] = "Employee Is Created Successfully";
				}
				else
					TempData["Message"] = "An Error ,Employee Not Created";

				return RedirectToAction(nameof(Index));
			}
			return View(employeeVM);
		}
		 
		[HttpGet]
		public async Task<IActionResult> Details(int? id, string ViewName = "Details")
		{
			//ViewData["Departments"] = _departmentRepo.GetAll();

			if (!id.HasValue)
				return BadRequest();
			var employee = await _unitOfWork.Repository<Employee>().GetAsync(id.Value);
			var MappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);

			if (employee is null)
				return NotFound();
			if (ViewName.Equals("Delete", StringComparison.OrdinalIgnoreCase))
				TempData["ImageName"] = employee.ImageName;

			return View(ViewName, MappedEmp);
		}

		[HttpGet]

		public async Task<IActionResult> Edit(int? id)
		{
			//ViewData["Departments"] = _departmentRepo.GetAll();

			return await Details(id, "Edit");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVM)
		{
			if (id != employeeVM.Id)
				return BadRequest();
			if (!ModelState.IsValid)
				return View(employeeVM);
			try
			{
				var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

				_unitOfWork.Repository<Employee>().Update(MappedEmp);
				_unitOfWork.Complete();
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
				return View(employeeVM);
			}
		}

		public async Task<IActionResult> Delete(int? id)
		{
			//ViewData["Departments"] = _departmentRepo.GetAll();
			return await Details(id, "Delete");
		}

		[HttpPost]
		public async Task<IActionResult> Delete(EmployeeViewModel employeeVm)
		{
			try
			{
				employeeVm.ImageName = TempData["ImageName"] as string;
				var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVm);

				_unitOfWork.Repository<Employee>().Delete(MappedEmp);
				var count =await _unitOfWork.Complete();
				if (count > 0)
				{
					DocumentSettings.DeleteFile(employeeVm.ImageName, "Images");
					return RedirectToAction(nameof(Index));
				}
				return View(employeeVm);

			}
			catch (Exception ex)
			{
				if (_env.IsDevelopment())
					ModelState.AddModelError(string.Empty, ex.Message);
				else
					ModelState.AddModelError(string.Empty, "An Error During Update The Department");
				return View(employeeVm);
			}
		}
	}
}
