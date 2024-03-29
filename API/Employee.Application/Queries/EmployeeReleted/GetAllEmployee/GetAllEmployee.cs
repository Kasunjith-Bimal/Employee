﻿using Employee.Application.Queries.EmployeeReleted.GetAllAdmin;
using Employee.Application.Wrappers;
using Employee.Domain.Entities;
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

                    List<GetAllEmployeeResponseDetail> allEmployeeList = new List<GetAllEmployeeResponseDetail>();

                    foreach (var employee in allEmployee)
                    {

                        GetAllEmployeeResponseDetail employeeData = new GetAllEmployeeResponseDetail()
                        {
                            Email = employee.Email,
                            Address = employee.Address,
                            FullName = employee.FullName,
                            Id = employee.Id,
                            JoinDate = employee.JoinDate,
                            Salary = employee.Salary,
                            Telephone = employee.Telephone,
                            RoleType = RoleType.EMPLOYEE,
                            IsActive  = employee.IsActive

                        };

                        allEmployeeList.Add(employeeData);
                    }


                    var response = new GetAllEmployeeResponse
                    {
                        employees = allEmployeeList.OrderByDescending(x => x.JoinDate).ToList()
                    };

                    await context.RespondAsync(ResponseWrapper<GetAllEmployeeResponse>.Success("Successfuly get all employees", response));

                }
                else
                {
                    var response = new GetAllEmployeeResponse
                    {
                        employees = new List<GetAllEmployeeResponseDetail>()
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
