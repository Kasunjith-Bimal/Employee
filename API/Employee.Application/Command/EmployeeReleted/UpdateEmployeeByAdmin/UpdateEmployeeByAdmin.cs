using Employee.Application.Wrappers;
using Employee.Domain.Intefaces;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Employee.Application.Command.EmployeeReleted.UpdateEmployeeByAdmin
{
    public class UpdateEmployeeByAdmin : IConsumer<UpdateEmployeeByAdminCommand>
    {
        private readonly ILogger<UpdateEmployeeByAdmin> logger;
        private readonly IEmployeeService employeeService;
        private readonly IConfiguration configuration;

        public UpdateEmployeeByAdmin(ILogger<UpdateEmployeeByAdmin> logger, IEmployeeService employeeService, IConfiguration configuration)
        {
            this.logger = logger;
            this.employeeService = employeeService;
            this.configuration = configuration;
        }

        public async Task Consume(ConsumeContext<UpdateEmployeeByAdminCommand> context)
        {
            try
            {

                this.logger.LogInformation($"[UpdateEmployeeByAdmin] Received event");

                if (!string.IsNullOrEmpty(context.Message.Id))
                {
                    if (context.Message.Employee.Id == context.Message.Id)
                    {
                        var getEmployee = await this.employeeService.GetEmployeeByIdAsync(context.Message.Id);

                        if (getEmployee != null)
                        {
                            getEmployee.Email = context.Message.Employee.Email;
                            getEmployee.Address = context.Message.Employee.Address;
                            getEmployee.FullName = context.Message.Employee.FullName;
                            getEmployee.Salary = context.Message.Employee.Salary;
                            getEmployee.PhoneNumber = context.Message.Employee.Telephone;
                            getEmployee.Telephone = context.Message.Employee.Telephone;
                            getEmployee.JoinDate = context.Message.Employee.JoinDate;
                            getEmployee.Address = context.Message.Employee.Address;

                            this.logger.LogInformation($"[UpdateEmployeeByAdmin] EmployeeService UpdateEmployeeByAdmin method call employeeid : {context.Message.Employee.Id}");
                            var updatedEmployeeDetail = await this.employeeService.UpdateEmployee(getEmployee);
                           
                            if (updatedEmployeeDetail != null)
                            {
                                var existingRole = await this.employeeService.GetUserRolesAsync(updatedEmployeeDetail);
                                var updateRoleWithUser = await this.employeeService.RemoveAndAddRolesAsync(updatedEmployeeDetail, existingRole, context.Message.Employee.RoleType);

                                if(updateRoleWithUser != null)
                                {
                                    this.logger.LogInformation($"[UpdateEmployeeByAdmin] employee update successfully employee id : {updatedEmployeeDetail.Id}");

                                    var response = new UpdateEmployeeByAdminResponse
                                    {
                                        employee = new UpdateEmployeeByAdminDetailResponse
                                        {
                                            Id = updatedEmployeeDetail.Id,
                                            Address = updatedEmployeeDetail.Address,
                                            Email = updatedEmployeeDetail.Email,
                                            FullName = updatedEmployeeDetail.FullName,
                                            JoinDate = updatedEmployeeDetail.JoinDate,
                                            RoleType = context.Message.Employee.RoleType,
                                            Salary = updatedEmployeeDetail.Salary,
                                            Telephone = updatedEmployeeDetail.Telephone
                                        }
                                    };

                                    await context.RespondAsync(ResponseWrapper<UpdateEmployeeByAdminResponse>.Success("Employee update successfully.", response));
                                }
                                else
                                {
                                    this.logger.LogInformation($"[UpdateEmployeeByAdmin] Failed to update role employee id ; {context.Message.Employee.Id}");
                                    await context.RespondAsync(ResponseWrapper<UpdateEmployeeByAdminResponse>.Fail("Failed to update  employee role."));
                                }
                               

                            }
                            else
                            {
                                this.logger.LogInformation($"[UpdateEmployeeByAdmin] Failed to update employee id ; {context.Message.Employee.Id}");
                                await context.RespondAsync(ResponseWrapper<UpdateEmployeeByAdminResponse>.Fail("Failed to update employee."));

                            }
                        }
                        else
                        {
                            this.logger.LogInformation($"[UpdateEmployeeByAdmin] Invalid employee Id ; {context.Message.Employee.Id}");
                            await context.RespondAsync(ResponseWrapper<UpdateEmployeeByAdminResponse>.Fail("Invalid employee Id."));
                        }

                    }
                    else
                    {
                        this.logger.LogInformation($"[UpdateEmployeeByAdmin] Id missmatch");
                        await context.RespondAsync(ResponseWrapper<UpdateEmployeeByAdminResponse>.Fail("Id missmatch"));
                    }
                }
                else
                {
                    this.logger.LogInformation($"[UpdateEmployeeByAdmin] Id cannot be empty");
                    await context.RespondAsync(ResponseWrapper<UpdateEmployeeByAdminResponse>.Fail("Id cannot be empty"));
                }

            }
            catch (Exception ex)
            {
                this.logger.LogDebug(ex, $"[UpdateEmployeeByAdmin] id : {context.Message.Employee.Id} - exception occored. stacktrace: {ex.StackTrace}");
                await context.RespondAsync(ResponseWrapper<UpdateEmployeeByAdminResponse>.Fail(ex.Message));
            }
        }
    }
}
