using System.ComponentModel.DataAnnotations;

namespace Day03.MVC.PL.ViewModels.Account
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "New Password Is Required")]
		[MinLength(5, ErrorMessage = "Minimum Length is 5")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }
		[Required(ErrorMessage = "Confirm Password Is Required")]
		[DataType(DataType.Password)]
		[Compare(nameof(NewPassword), ErrorMessage = "Confirm Password Dosn't Match with New password")]
		public string ConfirmPassword { get; set; }

      
    }
}
