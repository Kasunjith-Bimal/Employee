using Employee.Application.Wrappers;
using Employee.Domain.Entities;
using Employee.Domain.Enum;
using Employee.Domain.Intefaces;
using MassTransit;
using MassTransit.Testing.Implementations;
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

                    List<GetAllAdminResponseDetail> allEmployeeList = new List<GetAllAdminResponseDetail>();

                    foreach (var employee in allAdminEmployes)
                    {

                        GetAllAdminResponseDetail employeeData = new GetAllAdminResponseDetail()
                        {
                            Email = employee.Email,
                            Address = employee.Address,
                            FullName = employee.FullName,
                            Id  = employee.Id,
                            JoinDate = employee.JoinDate,
                            Salary = employee.Salary,
                            Telephone = employee.Telephone,
                            RoleType = RoleType.ADMIN

                        };

                        allEmployeeList.Add(employeeData);
                    }

                    var response = new GetAllAdminResponse
                    {
                        employees = allEmployeeList.OrderByDescending(x => x.JoinDate).ToList()
                    };

                    await context.RespondAsync(ResponseWrapper<GetAllAdminResponse>.Success("Successfuly get all employees", response));

                }
                else
                {
                    var response = new GetAllAdminResponse
                    {
                        employees = new List<GetAllAdminResponseDetail>()
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
