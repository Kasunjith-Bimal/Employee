using Employee.Domain.Entities;
using Employee.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Domain.Intefaces
{
    public interface IEmployeeService
    {
        Task<EmployeeDetail> GetEmployeeByIdAsync(string employeeId);

        Task<EmployeeDetail> GetEmployeeByEmailAsync(string email);
        Task<List<EmployeeDetail>> GetAllEmployeesAsync();

        Task<IList<string>> GetUserRolesAsync(EmployeeDetail employee);
        Task<EmployeeDetail> AddEmployee(EmployeeDetail employee, RoleType roleType);
        Task<EmployeeDetail> UpdateEmployee(EmployeeDetail employee);
        Task<bool> DeleteEmployee(EmployeeDetail employee);

        Task SendEmailAsync(string email, string subject, string message, string fullName);

        Task<List<EmployeeDetail>> GetAllAdminsAsync();

        Task<bool> CheckPasswordAsync(EmployeeDetail employee, string passWord);

    }
}
