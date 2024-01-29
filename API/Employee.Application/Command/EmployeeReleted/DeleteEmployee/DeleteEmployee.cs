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


namespace Employee.Application.Command.EmployeeReleted.DeleteEmployee
{
    public class DeleteEmployee : IConsumer<DeleteEmployeeCommand>
    {
        private readonly ILogger<DeleteEmployee> logger;
        private readonly IEmployeeService employeeService;
        private readonly IConfiguration configuration;

        public DeleteEmployee(ILogger<DeleteEmployee> logger, IEmployeeService employeeService, IConfiguration configuration)
        {
            this.logger = logger;
            this.employeeService = employeeService;
            this.configuration = configuration;
        }

        public async Task Consume(ConsumeContext<DeleteEmployeeCommand> context)
        {
            try
            {
                this.logger.LogInformation($"[DeleteEmployee] Received event");
                if (!string.IsNullOrEmpty(context.Message.Id))
                {
                    var getEmployee= await this.employeeService.GetEmployeeByIdAsync(context.Message.Id);

                    if (getEmployee != null)
                    {
                        this.logger.LogInformation($"[DeleteEmployee] employeeService DeleteEmployee method call");
                        var DeleteEmployee = await this.employeeService.DeleteEmployee(getEmployee);

                        if (DeleteEmployee)
                        {
                            this.logger.LogInformation($"[DeleteEmployee] Employee delete successfully employee id : {context.Message.Id}");

                            var response = new DeleteEmployeeResponse
                            {
                                IsDelete = DeleteEmployee
                            };

                            await context.RespondAsync(ResponseWrapper<DeleteEmployeeResponse>.Success("Employee delete successfully", response));

                        }
                        else
                        {
                            this.logger.LogInformation($"[DeleteEmployee] Failed to delete employee id {context.Message.Id}");
                            await context.RespondAsync(ResponseWrapper<DeleteEmployeeResponse>.Fail("Failed to delete employee."));

                        }
                    }
                    else
                    {
                        this.logger.LogInformation($"[DeleteEmployee] Invalid employee Id {context.Message.Id}");
                        await context.RespondAsync(ResponseWrapper<DeleteEmployeeResponse>.Fail("Invalid employee Id."));
                    }
                }
                else
                {
                    this.logger.LogInformation($"[DeleteEmployee] Id cannot be empty");
                    await context.RespondAsync(ResponseWrapper<DeleteEmployeeResponse>.Fail("Id cannot be empty"));

                } 
            }
            catch (Exception ex)
            {
                this.logger.LogDebug(ex, $"[DeleteEmployee] id: {context.Message.Id} - exception occored. stacktrace: {ex.StackTrace}");
                await context.RespondAsync(ResponseWrapper<DeleteEmployeeResponse>.Fail(ex.Message));
            }
        }
    }
}
