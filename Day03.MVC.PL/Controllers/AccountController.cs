using Day03.DAL.Models;
using Day03.MVC.PL.Services.EmailSender;
using Day03.MVC.PL.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Day03.MVC.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly IEmailSender _emailSender;
		private readonly IConfiguration _configuration;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(IEmailSender emailSender,IConfiguration configuration,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_emailSender = emailSender;
			_configuration = configuration;
			_userManager = userManager;
			_signInManager = signInManager;
		}
		#region Sign Up

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

		#region Sign Out

		public async new Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();

			return RedirectToAction(nameof(SignIn));
		}
		#endregion

		public IActionResult ForgetPassword()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SendResetPasswordEmail(ForgetPasswordViewModel model)
		{
			if(ModelState.IsValid)
			{
				var User =await _userManager.FindByEmailAsync(model.Email); 
				if(User is not null)
				{
					var resetPasswordToken=await _userManager.GeneratePasswordResetTokenAsync(User);
					var resetPasswordUrl = Url.Action("ResetPassword", "Account", new { email = User.Email,token= resetPasswordToken });
					await _emailSender.SendAsync(_configuration["EmailSettings:SenderEmail"], model.Email, "Reset your Password", resetPasswordUrl);
					return RedirectToAction(nameof(CheckYourInbox));
				}
				ModelState.AddModelError(string.Empty, "There Is No Account With This Email");
			}
			return View(model);
		}

		public IActionResult CheckYourInbox()
		{
			return View();
		}

		[HttpGet]

		public IActionResult ResetPassword(string email,string token)
		{
			TempData["Email"] = email;
			TempData["Token"] = token;
			return View();
		}

		[HttpPost]

		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var email = TempData["Email"] as string;
				var token = TempData["Token"] as string;
				var user = await _userManager.FindByEmailAsync(email);
				if(user is not null) 
				{ 

				await _userManager.ResetPasswordAsync(user,token,model.NewPassword);

					return RedirectToAction(nameof(SignIn)); 
				}
				ModelState.AddModelError(string.Empty, "Url Is Not Valid");
			}
			return View(model);
		}


	}
}
