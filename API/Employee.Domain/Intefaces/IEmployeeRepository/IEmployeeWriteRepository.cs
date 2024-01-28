using Employee.Domain.Entities;
using Employee.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Employee.Domain.Intefaces.IEmployeeRepository
{
    public interface IEmployeeWriteRepository
    {
        Task<EmployeeDetail> AddEmployee(EmployeeDetail employee, string tempoyryPassword, RoleType roleType);
        Task<EmployeeDetail> UpdateEmployee(EmployeeDetail employee);
        Task<bool> DeleteEmployee(long employeeId);

    }
}
