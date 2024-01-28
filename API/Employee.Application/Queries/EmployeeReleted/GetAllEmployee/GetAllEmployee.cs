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


namespace Employee.Application.Queries.EmployeeReleted.GetAllEmployee
{
    public class GetAllEmployee : IConsumer<GetAllEmployeeQuery>
    {
        private readonly ILogger<GetAllEmployee> logger;
        private readonly IEmployeeService employeeService;
        private readonly IConfiguration configuration;
        public GetAllEmployee(ILogger<GetAllEmployee> logger, IEmployeeService employeeService, IConfiguration configuration)
        {
            this.logger = logger;
            this.employeeService = employeeService;
            this.configuration = configuration;
        }

        public async Task Consume(ConsumeContext<GetAllEmployeeQuery> context)
        {
            try
            {
                this.logger.LogInformation($"[GetAllEmployee] Received event");
               
                this.logger.LogInformation($"[GetAllEmployee] TaskService GetAllEmployeesAsync method call");
                var allEmployee =  await this.employeeService.GetAllEmployeesAsync();

                if (allEmployee != null)
                {
                    this.logger.LogInformation($"[GetAllEmployee] Successfuly get all tasks");

                    var response = new GetAllEmployeeResponse
                    {
                        employees = allEmployee.OrderByDescending(x => x.JoinDate).ToList()
                    };

                    await context.RespondAsync(ResponseWrapper<GetAllEmployeeResponse>.Success("Successfuly get all employees", response));

                }
                else
                {
                    var response = new GetAllEmployeeResponse
                    {
                        employees = new List<EmployeeDetail>()
                    };

                    await context.RespondAsync(ResponseWrapper<GetAllEmployeeResponse>.Success("Successfuly get all employees", response));

                }
            }
            catch (Exception ex)
            {
                this.logger.LogDebug(ex, $"[GetAllEmployee] - exception occored. stacktrace: {ex.StackTrace}");
                await context.RespondAsync(ResponseWrapper<GetAllEmployeeResponse>.Fail(ex.Message));
            }
        }
    }
}
