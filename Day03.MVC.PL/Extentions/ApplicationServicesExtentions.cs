using Day03.BLL.Interfaces;
using Day03.BLL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Day03.MVC.PL.Extentions
{
	public static class ApplicationServicesExtentions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services )
		{
			services.AddScoped<IDepartmentRepositories, DepartmentRepositories>();
			services.AddScoped<IEmployeeRepository, EmployeeRepository>();
			return services;
		}
	}
}
