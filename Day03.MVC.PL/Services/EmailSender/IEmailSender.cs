using System.Threading.Tasks;

namespace Day03.MVC.PL.Services.EmailSender
{
	public interface IEmailSender
	{
		Task SendAsync(string form,string recipients,string subject,string body);
	}
}
