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
        Task<EmployeeDetail> GetEmployeByIdAsync(long employeeId);
        Task<List<EmployeeDetail>> GetAlEmployesAsync();
    }
}
