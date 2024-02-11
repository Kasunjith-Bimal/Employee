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

        public Task<EmployeeDetail> AddRoleToEmployee(EmployeeDetail employee, RoleType roleType)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ChangePasswordAsync(EmployeeDetail employee, string oldPassword, string NewPassword)
        {
            try
            {
                var changePasswordResult = await _userManager.ChangePasswordAsync(employee, oldPassword, NewPassword);
                if (changePasswordResult.Succeeded)
                {
                    return await Task.FromResult(true);
                }
                else
                {
                    return await Task.FromResult(false);
                }
            }
            catch (Exception ex)
            {

                return await Task.FromResult(false);
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

        public async Task<EmployeeDetail> RemoveAndAddRolesAsync(EmployeeDetail employee, IList<string> oldRoleType, RoleType newRoleType)
        {
            try
            {
               
                var result =  await _userManager.RemoveFromRolesAsync(employee, oldRoleType);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(employee, Enum.GetName(typeof(RoleType), newRoleType));
                    return employee;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
            
        }

        public async Task<EmployeeDetail> UpdateEmployee(EmployeeDetail employee)
        {
            try
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
            catch (Exception ex)
            {

                return null;
            }
           
        }
    }
}
