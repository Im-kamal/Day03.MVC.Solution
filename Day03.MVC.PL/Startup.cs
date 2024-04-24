using Day03.BLL.Interfaces;
using Day03.BLL.Repositories;
using Day03.DAL.Data;
using Day03.DAL.Models;
using Day03.MVC.PL.Extentions;
using Day03.MVC.PL.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
namespace Day03.MVC.PL
{
	public class Startup 
	{
		public IConfiguration Configuration { get; }
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		} 


		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();
			//services.AddTransient<ApplicationDbContext>();
			//services.AddSingleton<ApplicationDbContext>(); 

			// services.AddScoped<ApplicationDbContext>();
			//services.AddScoped<DbContextOptions<ApplicationDbContext>>();
			// ==>
			services.AddDbContext<ApplicationDbContext>(
				//contextLifetime : ServiceLifetime.Scoped,  //Default
				//optionsLifetime : ServiceLifetime.Scoped   //Default
				options => { options.UseSqlServer("Server=.;Database=MVCApplication;Trusted_Connection=True"); }
				);
			services.AddAutoMapper(M=>M.AddProfile(new MappingProfiles()));
			services.AddApplicationServices();
			//services.AddScoped<UserManager<ApplicationUser>>();
			//services.AddScoped<SignInManager<ApplicationUser>>();
			//services.AddScoped<RoleManager<IdentityRole>>();

			services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			{
				options.Password.RequiredUniqueChars = 2;
				options.Password.RequireDigit = true;
				options.Password.RequireNonAlphanumeric = true;

				options.User.RequireUniqueEmail = true;
			}).AddEntityFrameworkStores<ApplicationDbContext>();	  

			
			services.AddAuthentication();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
