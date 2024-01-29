using Employee.Application.Wrappers;
using Employee.Domain.Entities;
using Employee.Domain.Intefaces;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Employee.Application.Command.AuthenticationReleted.Register
{
    public class RegisterEmployee : IConsumer<RegisterEmployeeCommand>
    {
        private readonly ILogger<RegisterEmployee> logger;
        private readonly IConfiguration configuration;
        private readonly IEmployeeService employeeService;
        public RegisterEmployee(ILogger<RegisterEmployee> logger, IConfiguration configuration,IEmployeeService employeeService)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.employeeService = employeeService;
        }

        public async Task Consume(ConsumeContext<RegisterEmployeeCommand> context)
        {
            try
            {
                this.logger.LogInformation($"[RegisterEmployee] Received event");
                this.logger.LogInformation($"[RegisterEmployee] Check Email is Empty");
                if (!String.IsNullOrEmpty(context.Message.Email))
                {
                    this.logger.LogInformation($"[RegisterEmployee] Check Email is exsist");
                    var checkExistingEmail = await this.employeeService.GetEmployeeByEmailAsync(context.Message.Email);

                    if(checkExistingEmail == null)
                    {
                        this.logger.LogInformation($"[RegisterEmployee] Check Email is not exsist");
                        var employeeDetails = new EmployeeDetail
                        {
                            Address = context.Message.Address,
                            Email = context.Message.Email,
                            UserName = context.Message.Email,
                            Salary = context.Message.Salary,
                            FullName = context.Message.FirstName + " " + context.Message.LastName,
                            JoinDate = context.Message.JoinDate,
                            Telephone = context.Message.Telephone,
                            PhoneNumber = context.Message.Telephone,
                            IsFirstLogin = true,
                        };

                        var employee = await this.employeeService.AddEmployee(employeeDetails, context.Message.RoleType);

                        if (employee != null)
                        {
                            var response = new RegisterEmployeeResponse
                            {
                                employee = employee
                            };

                            this.logger.LogInformation($"[RegisterEmployee] Success fully add employee");
                            await context.RespondAsync(ResponseWrapper<RegisterEmployeeResponse>.Success("Success fully add employee", response));
                        }
                        else
                        {
                            this.logger.LogInformation($"[RegisterEmployee] Fail to add employee");
                            await context.RespondAsync(ResponseWrapper<RegisterEmployeeResponse>.Fail("Fail to add employee"));
                        }
                    }
                    else
                    {
                        this.logger.LogInformation($"[RegisterEmployee] Fail to add employee email alredy existe");
                        await context.RespondAsync(ResponseWrapper<RegisterEmployeeResponse>.Fail("Fail to add employee email alredy existe"));
                    }
                }
                else
                {
                    this.logger.LogInformation($"[RegisterEmployee] Email cannot be empty");
                    await context.RespondAsync(ResponseWrapper<RegisterEmployeeResponse>.Fail("Email cannot be empty"));
                }
            }
            catch (Exception ex)
            {
                this.logger.LogDebug(ex, $"[RegisterEmployee] - exception occored. stacktrace: {ex.StackTrace}");
                await context.RespondAsync(ResponseWrapper<RegisterEmployeeResponse>.Fail(ex.Message));
            }
        }
    }
}
