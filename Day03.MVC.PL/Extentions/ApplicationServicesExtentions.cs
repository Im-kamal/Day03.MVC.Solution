using Day03.BLL;
using Day03.BLL.Interfaces;
using Day03.BLL.Repositories;
using Day03.MVC.PL.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Day03.MVC.PL.Extentions 
{  
	public static class ApplicationServicesExtentions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services )
		{
			//services.AddScoped<IDepartmentRepositories, DepartmentRepositories>();
			//services.AddScoped<IEmployeeRepository, EmployeeRepository>();
			//services.AddAutoMapper (M=>M.AddProfile(new MappingProfiles() ));
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			return services;
		}
	}
}
