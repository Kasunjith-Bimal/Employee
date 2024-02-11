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


namespace Employee.Application.Queries.EmployeeReleted.GetEmployeeById
{
    public class GetEmployeeById : IConsumer<GetEmployeeByIdQuery>
    {
        private readonly ILogger<GetEmployeeById> logger;
        private readonly IEmployeeService employeeService;
        private readonly IConfiguration configuration;
        public GetEmployeeById(ILogger<GetEmployeeById> logger, IEmployeeService employeeService, IConfiguration configuration)
        {
            this.logger = logger;
            this.employeeService = employeeService;
            this.configuration = configuration;
        }

        public async Task Consume(ConsumeContext<GetEmployeeByIdQuery> context)
        {
            try
            {
                this.logger.LogInformation($"[GetEmployeeById] Received event");
                if (!string.IsNullOrEmpty(context.Message.Id))
                {
                    this.logger.LogInformation($"[GetEmployeeById] EmployeeService GetEmployeeByIdAsync method call");
                    var findEmployee = await this.employeeService.GetEmployeeByIdAsync(context.Message.Id);

                    if (findEmployee != null)
                    {
                        var roleType = await this.employeeService.GetUserRolesAsync(findEmployee);
                        this.logger.LogInformation($"[GetEmployeeById] Successfuly get employee id {context.Message.Id}");

                        var response = new GetEmployeeByIdResponse
                        {
                            employee = new GetEmployeeByIdDetail
                            {
                                Id = findEmployee.Id,
                                Email = findEmployee.Email,
                                Address = findEmployee.Address,
                                FullName = findEmployee.FullName,
                                JoinDate = findEmployee.JoinDate,
                                Salary = findEmployee.Salary,
                                Telephone = findEmployee.Telephone,
                                RoleType = roleType[0].ToLower() == "admin"? Domain.Enum.RoleType.ADMIN : Domain.Enum.RoleType.EMPLOYEE
                            }
                        };

                        await context.RespondAsync(ResponseWrapper<GetEmployeeByIdResponse>.Success("Successfuly get employee", response));

                    }
                    else
                    {
                        this.logger.LogInformation($"[GetEmployeeById] Failed to get employee id {context.Message.Id}");
                        await context.RespondAsync(ResponseWrapper<GetEmployeeByIdResponse>.Fail("Failed to get employee Invalid employee Id"));

                    }
                }
                else
                {
                    this.logger.LogInformation($"[GetEmployeeById] Invalid employee Id {context.Message.Id}");
                    await context.RespondAsync(ResponseWrapper<GetEmployeeByIdResponse>.Fail("Invalid employee Id"));
                }
                
            }
            catch (Exception ex)
            {
                this.logger.LogDebug(ex, $"[GetEmployeeById] id {context.Message.Id} - exception occored. stacktrace: {ex.StackTrace}");
                await context.RespondAsync(ResponseWrapper<GetEmployeeByIdResponse>.Fail(ex.Message));
            }
        }
    }
}
