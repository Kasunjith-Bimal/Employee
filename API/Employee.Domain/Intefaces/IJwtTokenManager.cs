using Employee.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Domain.Intefaces
{
    public interface IJwtTokenManager
    {
        Task<Tuple<string, DateTime>> GenerateToken(EmployeeDetail employee);


    }
}
