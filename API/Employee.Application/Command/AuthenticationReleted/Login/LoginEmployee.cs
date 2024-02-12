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


namespace Employee.Application.Command.AuthenticationReleted.Login
{
    public class LoginEmployee : IConsumer<LoginEmployeeCommand>
    {
        private readonly ILogger<LoginEmployee> logger;
        private readonly IConfiguration configuration;
        private readonly IEmployeeService employeeService;
        private readonly IJwtTokenManager JwtTokenManager;
        public LoginEmployee(ILogger<LoginEmployee> logger, IConfiguration configuration,IEmployeeService employeeService, IJwtTokenManager JwtTokenManager)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.employeeService = employeeService;
            this.JwtTokenManager = JwtTokenManager;
        }

        public async Task Consume(ConsumeContext<LoginEmployeeCommand> context)
        {
            try
            {
                this.logger.LogInformation($"[LoginEmployee] Received event");
                if ((!String.IsNullOrEmpty(context.Message.Password))){
                    this.logger.LogInformation($"[LoginEmployee] Check Email is Empty");
                    if (!String.IsNullOrEmpty(context.Message.Email))
                    {
                        this.logger.LogInformation($"[LoginEmployee] Check Email is exsist");
                        var checkExistingEmployee = await this.employeeService.GetEmployeeByEmailAsync(context.Message.Email);
                        if (checkExistingEmployee.IsActive)
                        {
                            if (checkExistingEmployee != null)
                            {
                                var chekPasswordCurrect = await this.employeeService.CheckPasswordAsync(checkExistingEmployee, context.Message.Password);

                                if (chekPasswordCurrect)
                                {
                                    // generatee Token 
                                    var generateAccesstokenDetails = await this.JwtTokenManager.GenerateToken(checkExistingEmployee);

                                    if (generateAccesstokenDetails != null)
                                    {
                                        var response = new LoginEmployeeResponse
                                        {
                                            TokenDetail = new LoginEmployeeTokenResponse
                                            {
                                                AccessToken = generateAccesstokenDetails.Item1,
                                                Expire = generateAccesstokenDetails.Item2,
                                            },
                                            Email = checkExistingEmployee.Email,
                                            FullName = checkExistingEmployee.FullName,
                                            IsFirstLogin = checkExistingEmployee.IsFirstLogin,
                                            Id = checkExistingEmployee.Id
                                        };

                                        this.logger.LogInformation($"[LoginEmployee] success fully login");
                                        await context.RespondAsync(ResponseWrapper<LoginEmployeeResponse>.Success("success fully login", response));
                                    }
                                    else
                                    {
                                        this.logger.LogInformation($"[LoginEmployee] Fail to generate Token");
                                        await context.RespondAsync(ResponseWrapper<LoginEmployeeResponse>.Fail("Fail to generate Token"));
                                    }
                                }
                                else
                                {
                                    this.logger.LogInformation($"[LoginEmployee] password mismatch");
                                    await context.RespondAsync(ResponseWrapper<LoginEmployeeResponse>.Fail("password not found"));
                                }
                            }
                            else
                            {
                                this.logger.LogInformation($"[LoginEmployee] Email address mismatch");
                                await context.RespondAsync(ResponseWrapper<LoginEmployeeResponse>.Fail("Email address not found"));
                            }
                        }
                        else
                        {
                            this.logger.LogInformation($"[LoginEmployee] Inactive user");
                            await context.RespondAsync(ResponseWrapper<LoginEmployeeResponse>.Fail("Inactive user"));

                        }
                       
                    }
                    else
                    {
                        this.logger.LogInformation($"[LoginEmployee] Email cannot be empty");
                        await context.RespondAsync(ResponseWrapper<LoginEmployeeResponse>.Fail("Email cannot be empty"));
                    }
                }
                else
                {
                    this.logger.LogInformation($"[LoginEmployee] password cannot be empty");
                    await context.RespondAsync(ResponseWrapper<LoginEmployeeResponse>.Fail("password cannot be empty"));
                }
             
            }
            catch (Exception ex)
            {
                this.logger.LogDebug(ex, $"[LoginEmployee] - exception occored. stacktrace: {ex.StackTrace}");
                await context.RespondAsync(ResponseWrapper<LoginEmployeeResponse>.Fail(ex.Message));
            }
        }
    }
}
