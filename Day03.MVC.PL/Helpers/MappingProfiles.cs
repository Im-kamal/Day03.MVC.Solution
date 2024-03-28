using AutoMapper;
using Day03.DAL.Models;
using Day03.MVC.PL.ViweModels;

namespace Day03.MVC.PL.Helpers
{
	public class MappingProfiles:Profile
	{
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();  
        }
    }
}
