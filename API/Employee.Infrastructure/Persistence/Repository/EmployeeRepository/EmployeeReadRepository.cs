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

        public async Task<List<EmployeeDetail>> GetAllEmployesAsync()
        {
            try
            {
                var employeeList = await this._userManager.GetUsersInRoleAsync("EMPLOYEE");

                if(employeeList != null)
                {
                    return (List<EmployeeDetail>)employeeList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<EmployeeDetail> GetEmployeByIdAsync(string employeeId)
        {
            try
            {
                var employee = await this._userManager.FindByIdAsync(employeeId);

                if (employee != null)
                {
                    return employee;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<EmployeeDetail> GetEmployeByEmailAsync(string employeeEmail)
        {
            try
            {
                var employee = await this._userManager.FindByEmailAsync(employeeEmail);

                if (employee != null)
                {
                    return employee;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<List<EmployeeDetail>> GetAllAdminsAsync()
        {
            try
            {
                var employeeList = await this._userManager.GetUsersInRoleAsync("ADMIN");

                if (employeeList != null)
                {
                    return (List<EmployeeDetail>)employeeList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<bool> CheckPasswordAsync(EmployeeDetail employee, string passWord)
        {
            try
            {
                return await _userManager.CheckPasswordAsync(employee, passWord);
            }
            catch (Exception)
            {

                return await Task.FromResult(false);
            }
        }

        public async Task<IList<string>> GetUserRolesAsync(EmployeeDetail employee)
        {
            try
            {
                return await _userManager.GetRolesAsync(employee);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
