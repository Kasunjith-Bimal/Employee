using Employee.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Domain.Intefaces.IEmployeeRepository
{
    public interface IEmployeeReadRepository
    {
        Task<EmployeeDetail> GetEmployeByIdAsync(string employeeId);

        Task<EmployeeDetail> GetEmployeByEmailAsync(string employeeEmail);

        Task<List<EmployeeDetail>> GetAllEmployesAsync();

        Task<List<EmployeeDetail>> GetAllAdminsAsync();

        Task<IList<string>> GetUserRolesAsync(EmployeeDetail employee);


        Task<bool> CheckPasswordAsync(EmployeeDetail employee ,string passWord);
    }
}
