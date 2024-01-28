using Employee.Domain.Entities;
using Employee.Domain.Enum;
using Employee.Domain.Intefaces.IEmployeeRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Employee.Infrastructure.Persistence.Repository.EmployeeRepository
{
    public class EmployeeWriteRepository : IEmployeeWriteRepository
    {
      
        private readonly ILogger<EmployeeWriteRepository> logger;
        private readonly UserManager<EmployeeDetail> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EmployeeWriteRepository(ILogger<EmployeeWriteRepository> logger, UserManager<EmployeeDetail> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.logger = logger;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task<EmployeeDetail> AddEmployee(EmployeeDetail employee,string tempoyryPassword,RoleType roleType)
        {
            var result = await _userManager.CreateAsync(employee, tempoyryPassword);
            if (result.Succeeded)
            {
                  await _userManager.AddToRoleAsync(employee, Enum.GetName(typeof(RoleType), roleType));
                  return employee;
            }
            else
            {
                return null;
            }
                
        }

        public async Task<bool> DeleteEmployee(EmployeeDetail employee)
        {
            var result = await _userManager.DeleteAsync(employee);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<EmployeeDetail> UpdateEmployee(EmployeeDetail employee)
        {
            var result = await _userManager.UpdateAsync(employee);
            if (result.Succeeded)
            {
                return employee;
            }
            else
            {
                return null;
            }
        }
    }
}
