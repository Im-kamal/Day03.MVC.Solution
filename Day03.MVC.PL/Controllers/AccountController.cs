using Day03.DAL.Models;
using Day03.MVC.PL.ViewModels.Account;
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

		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user=await _userManager.FindByEmailAsync(model.Email);
				if(user is not null)
				{
					var flag=await _userManager.CheckPasswordAsync(user, model.Password);
					if(flag)
					{
						var Result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

						if(Result.IsLockedOut)
							ModelState.AddModelError(string.Empty, "Your Account Is Locked!!");
						if (Result.Succeeded) 
							return RedirectToAction(nameof(HomeController.Index),"Home");
						if(Result.IsNotAllowed)
							ModelState.AddModelError(string.Empty, "Your Account iS not Confirmed yet!!");

					}
				}
				ModelState.AddModelError(string.Empty, "Invalid Login");
			}
			return View(model);
		}
		#endregion
	}
}
