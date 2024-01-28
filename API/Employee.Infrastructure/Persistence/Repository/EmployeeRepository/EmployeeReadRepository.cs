using Employee.Domain.Entities;
using Employee.Domain.Intefaces.IEmployeeRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Employee.Infrastructure.Persistence.Repository.EmployeeRepository
{
    public class EmployeeReadRepository : IEmployeeReadRepository
    {
     
        private readonly ILogger<EmployeeReadRepository> logger;
        private readonly UserManager<EmployeeDetail> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EmployeeReadRepository( ILogger<EmployeeReadRepository> logger, UserManager<EmployeeDetail> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.logger = logger;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task<List<EmployeeDetail>> GetAlEmployesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<EmployeeDetail> GetEmployeByIdAsync(long employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
