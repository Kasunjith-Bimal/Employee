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

                if (!string.IsNullOrEmpty(context.Message.employee.Id))
                {
                    if (context.Message.employee.Id == context.Message.Id)
                    {
                                var getTask = await this.employeeService.GetEmployeeByIdAsync(context.Message.Id);

                                if (getTask != null)
                                {
                                    this.logger.LogInformation($"[UpdateEmployeeByAdmin] EmployeeService UpdateEmployeeByAdmin method call employeeid : {context.Message.employee.Id}");
                                    var updatedEmployeeDetail = await this.employeeService.UpdateEmployee(context.Message.employee);

                                    if (updatedEmployeeDetail != null)
                                    {
                                        this.logger.LogInformation($"[UpdateEmployeeByAdmin] employee update successfully employee id : {updatedEmployeeDetail.Id}");

                                        var response = new UpdateEmployeeByAdminResponse
                                        {
                                            employee = updatedEmployeeDetail
                                        };

                                        await context.RespondAsync(ResponseWrapper<UpdateEmployeeByAdminResponse>.Success("Employee update successfully.", response));

                                    }
                                    else
                                    {
                                        this.logger.LogInformation($"[UpdateEmployeeByAdmin] Failed to update employee id ; {context.Message.employee.Id}");
                                        await context.RespondAsync(ResponseWrapper<UpdateEmployeeByAdminResponse>.Fail("Failed to update employee."));

                                    }
                                }
                                else
                                {
                                    this.logger.LogInformation($"[UpdateEmployeeByAdmin] Invalid employee Id ; {context.Message.employee.Id}");
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
                this.logger.LogDebug(ex, $"[UpdateEmployeeByAdmin] id : {context.Message.employee.Id} - exception occored. stacktrace: {ex.StackTrace}");
                await context.RespondAsync(ResponseWrapper<UpdateEmployeeByAdminResponse>.Fail(ex.Message));
            }
        }
    }
}
