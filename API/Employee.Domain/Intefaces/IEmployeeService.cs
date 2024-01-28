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
        Task<EmployeeDetail> GetEmployeeByIdAsync(long taskId);
        Task<List<EmployeeDetail>> GetAllEmployeesAsync();
        Task<EmployeeDetail> AddEmployee(EmployeeDetail task, RoleType roleType);
        Task<EmployeeDetail> UpdateEmployee(EmployeeDetail task);
        Task<bool> DeleteEmployee(long employeId);

        Task SendEmailAsync(string email, string subject, string message, string fullName);

    }
}
