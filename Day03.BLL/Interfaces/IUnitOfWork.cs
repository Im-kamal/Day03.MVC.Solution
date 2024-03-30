using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03.BLL.Interfaces
{
	public interface IUnitOfWork:IDisposable 
	{
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepositories DepartmentRepository { get; set; }
        int Complete(); 
    }
}
