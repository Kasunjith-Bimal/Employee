using Employee.Application.Wrappers;
using Employee.Domain.Enum;
using Employee.Domain.Intefaces;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Employee.Application.Command.EmployeeReleted.UpdateEmployeeByEmployee
{
    public class UpdateEmployeeByEmployee : IConsumer<UpdateEmployeeByEmployeeCommand>
    {
        private readonly ILogger<UpdateEmployeeByEmployee> logger;
        private readonly IEmployeeService employeeService;
        private readonly IConfiguration configuration;

        public UpdateEmployeeByEmployee(ILogger<UpdateEmployeeByEmployee> logger, IEmployeeService employeeService, IConfiguration configuration)
        {
            this.logger = logger;
            this.employeeService = employeeService;
            this.configuration = configuration;
        }

        public async Task Consume(ConsumeContext<UpdateEmployeeByEmployeeCommand> context)
        {
            try
            {

                this.logger.LogInformation($"[UpdateEmployeeByEmployee] Received event");

                if (!string.IsNullOrEmpty(context.Message.Id))
                {
                    if (context.Message.employee.Id == context.Message.Id)
                    {
                        var getEmployee = await this.employeeService.GetEmployeeByIdAsync(context.Message.Id);

                        if (getEmployee != null)
                        {
                            //update current value
                            getEmployee.FullName = context.Message.employee.FullName;
                            getEmployee.Address = context.Message.employee.Address;
                            getEmployee.Telephone = context.Message.employee.Telephone;
                            getEmployee.PhoneNumber = context.Message.employee.Telephone;


                            this.logger.LogInformation($"[UpdateEmployeeByEmployee] EmployeeService UpdateEmployeeByEmployee method call employeeid : {context.Message.employee.Id}");
                            var updatedEmployeeDetail = await this.employeeService.UpdateEmployee(getEmployee);

                            if (updatedEmployeeDetail != null)
                            {
                                this.logger.LogInformation($"[UpdateEmployeeByEmployee] employee update successfully employee id : {updatedEmployeeDetail.Id}");

                                var response = new UpdateEmployeeByEmployeeResponse
                                {
                                    employee = new UpdateEmployeeByEmployeeDetilResponse
                                    {
                                        Email = updatedEmployeeDetail.Email,
                                        Address= updatedEmployeeDetail.Address,
                                        FullName = updatedEmployeeDetail.FullName,
                                        Id = updatedEmployeeDetail.Id,
                                        IsActive = updatedEmployeeDetail.IsActive,
                                        JoinDate = updatedEmployeeDetail.JoinDate,
                                        RoleType = RoleType.EMPLOYEE,
                                        Salary = updatedEmployeeDetail.Salary,
                                        Telephone = updatedEmployeeDetail.Telephone,
         
                                    }
                                };

                                await context.RespondAsync(ResponseWrapper<UpdateEmployeeByEmployeeResponse>.Success("Employee update successfully.", response));

                            }
                            else
                            {
                                this.logger.LogInformation($"[UpdateEmployeeByEmployee] Failed to update employee id ; {context.Message.employee.Id}");
                                await context.RespondAsync(ResponseWrapper<UpdateEmployeeByEmployeeResponse>.Fail("Failed to update employee."));

                            }
                        }
                        else
                        {
                            this.logger.LogInformation($"[UpdateEmployeeByEmployee] Invalid employee Id ; {context.Message.employee.Id}");
                            await context.RespondAsync(ResponseWrapper<UpdateEmployeeByEmployeeResponse>.Fail("Invalid employee Id."));
                        }

                    }
                    else
                    {
                        this.logger.LogInformation($"[UpdateEmployeeByEmployee] Id missmatch");
                        await context.RespondAsync(ResponseWrapper<UpdateEmployeeByEmployeeResponse>.Fail("Id missmatch"));
                    }
                }
                else
                {
                    this.logger.LogInformation($"[UpdateEmployeeByEmployee] Id cannot be empty");
                    await context.RespondAsync(ResponseWrapper<UpdateEmployeeByEmployeeResponse>.Fail("Id cannot be empty"));
                }

            }
            catch (Exception ex)
            {
                this.logger.LogDebug(ex, $"[UpdateEmployeeByEmployee] id : {context.Message.employee.Id} - exception occored. stacktrace: {ex.StackTrace}");
                await context.RespondAsync(ResponseWrapper<UpdateEmployeeByEmployeeResponse>.Fail(ex.Message));
            }
        }
    }
}
