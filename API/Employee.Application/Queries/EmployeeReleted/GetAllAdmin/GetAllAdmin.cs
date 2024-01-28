using Employee.Application.Wrappers;
using Employee.Domain.Entities;
using Employee.Domain.Intefaces;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Employee.Application.Queries.EmployeeReleted.GetAllAdmin
{
    public class GetAllAdmin : IConsumer<GetAllAdminQuery>
    {
        private readonly ILogger<GetAllAdmin> logger;
        private readonly IEmployeeService employeeService;
        private readonly IConfiguration configuration;
        public GetAllAdmin(ILogger<GetAllAdmin> logger, IEmployeeService employeeService, IConfiguration configuration)
        {
            this.logger = logger;
            this.employeeService = employeeService;
            this.configuration = configuration;
        }

        public async Task Consume(ConsumeContext<GetAllAdminQuery> context)
        {
            try
            {
                this.logger.LogInformation($"[GetAllAdmin] Received event");
               
                this.logger.LogInformation($"[GetAllAdmin] Employee Service GetAllAdminsAsync method call");
                var allAdminEmployes =  await this.employeeService.GetAllAdminsAsync();

                if (allAdminEmployes != null)
                {
                    this.logger.LogInformation($"[GetAllAdmin] Successfuly get all tasks");

                    var response = new GetAllAdminResponse
                    {
                        employees = allAdminEmployes.OrderByDescending(x => x.JoinDate).ToList()
                    };

                    await context.RespondAsync(ResponseWrapper<GetAllAdminResponse>.Success("Successfuly get all employees", response));

                }
                else
                {
                    var response = new GetAllAdminResponse
                    {
                        employees = new List<EmployeeDetail>()
                    };

                    await context.RespondAsync(ResponseWrapper<GetAllAdminResponse>.Success("Successfuly get all employees", response));
                }
            }
            catch (Exception ex)
            {
                this.logger.LogDebug(ex, $"[GetAllAdmin] - exception occored. stacktrace: {ex.StackTrace}");
                await context.RespondAsync(ResponseWrapper<GetAllAdminResponse>.Fail(ex.Message));
            }
        }
    }
}
