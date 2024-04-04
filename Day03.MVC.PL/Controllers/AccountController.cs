using Day03.DAL.Models;
using Day03.MVC.PL.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Day03.MVC.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		#region Region

		[HttpGet]
		public IActionResult SignUp()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (ModelState.IsValid)
			{
				var User = await _userManager.FindByNameAsync(model.UserName);
				if (User == null)
				{
					User = new ApplicationUser()
					{
						FName = model.FirstName,
						LName = model.LastName,
						UserName = model.UserName,
						Email = model.Email,
						IsAgree = model.IsAgree,
					};

					var result=await _userManager.CreateAsync(User,model.Password);
					if (result.Succeeded)
						return RedirectToAction(nameof(SignIn));
					foreach (var error in result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);
					
					 
				}
				ModelState.AddModelError(string.Empty, "this username is elready in use for another account!");
			}
			return View();
		}
		#endregion

		#region SignIn
		public IActionResult SignIn()
		{
			 return View();
		}
		#endregion
	}
}
