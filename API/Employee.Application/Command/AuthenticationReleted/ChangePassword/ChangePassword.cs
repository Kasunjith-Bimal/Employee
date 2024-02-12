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


namespace Employee.Application.Command.AuthenticationReleted.ChangePassword
{
    public class ChangePassword : IConsumer<ChangePasswordCommand>
    {
        private readonly ILogger<ChangePassword> logger;
        private readonly IConfiguration configuration;
        private readonly IEmployeeService employeeService;
        private readonly IJwtTokenManager JwtTokenManager;
        public ChangePassword(ILogger<ChangePassword> logger, IConfiguration configuration,IEmployeeService employeeService, IJwtTokenManager JwtTokenManager)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.employeeService = employeeService;
            this.JwtTokenManager = JwtTokenManager;
        }

        public async Task Consume(ConsumeContext<ChangePasswordCommand> context)
        {
            try
            {
                this.logger.LogInformation($"[ChangePassword] Received event");
                if ((!String.IsNullOrEmpty(context.Message.OldPassword))){

                    if ((!String.IsNullOrEmpty(context.Message.NewPassword)))
                    {
                        this.logger.LogInformation($"[ChangePassword] Check Email is Empty");
                        if (!String.IsNullOrEmpty(context.Message.Email))
                        {
                            this.logger.LogInformation($"[ChangePassword] Check Email is exsist");
                            var checkExistingEmployee = await this.employeeService.GetEmployeeByEmailAsync(context.Message.Email);

                            if (checkExistingEmployee != null)
                            {
                                if (checkExistingEmployee.IsActive)
                                {
                                    var chekOldPasswordCurrect = await this.employeeService.CheckPasswordAsync(checkExistingEmployee, context.Message.OldPassword);

                                    if (chekOldPasswordCurrect)
                                    {
                                        var updateNewPassword = await this.employeeService.ChangePasswordAsync(checkExistingEmployee, context.Message.OldPassword, context.Message.NewPassword);

                                        if (updateNewPassword)
                                        {
                                            var getUpdatedEmployee = await this.employeeService.GetEmployeeByIdAsync(checkExistingEmployee.Id);

                                            if (getUpdatedEmployee != null)
                                            {
                                                getUpdatedEmployee.IsFirstLogin = false;
                                                var updateEmployee = await this.employeeService.UpdateEmployee(getUpdatedEmployee);

                                                if (updateEmployee != null)
                                                {
                                                    var response = new ChangePasswordResponse
                                                    {
                                                        Email = updateEmployee.Email
                                                    };

                                                    this.logger.LogInformation($"[ChangePassword] success to change password");
                                                    await context.RespondAsync(ResponseWrapper<ChangePasswordResponse>.Success("success to change password", response));

                                                }
                                                else
                                                {
                                                    this.logger.LogInformation($"[ChangePassword] failed to update is first login employee");
                                                    await context.RespondAsync(ResponseWrapper<ChangePasswordResponse>.Fail("failed update is first login employee"));
                                                }
                                            }
                                            else
                                            {
                                                this.logger.LogInformation($"[ChangePassword] failed to get update employee");
                                                await context.RespondAsync(ResponseWrapper<ChangePasswordResponse>.Fail("failed to get update employee"));
                                            }

                                        }
                                        else
                                        {
                                            this.logger.LogInformation($"[ChangePassword] failed to update new password");
                                            await context.RespondAsync(ResponseWrapper<ChangePasswordResponse>.Fail("failed to update new password"));
                                        }


                                    }
                                    else
                                    {
                                        this.logger.LogInformation($"[ChangePassword] password mismatch");
                                        await context.RespondAsync(ResponseWrapper<ChangePasswordResponse>.Fail("password mismatch"));
                                    }
                                }
                                else
                                {
                                    this.logger.LogInformation($"[ChangePassword] in active user");
                                    await context.RespondAsync(ResponseWrapper<ChangePasswordResponse>.Fail("in active user"));
                                }
                                
                            }
                            else
                            {
                                this.logger.LogInformation($"[ChangePassword] Email address mismatch");
                                await context.RespondAsync(ResponseWrapper<ChangePasswordResponse>.Fail("Email address mismatch"));
                            }
                        }
                        else
                        {
                            this.logger.LogInformation($"[ChangePassword] Email cannot be empty");
                            await context.RespondAsync(ResponseWrapper<ChangePasswordResponse>.Fail("Email cannot be empty"));
                        }
                    }
                    else
                    {
                        this.logger.LogInformation($"[ChangePassword] new password cannot be empty");
                        await context.RespondAsync(ResponseWrapper<ChangePasswordResponse>.Fail("new password cannot be empty"));
                    }
                    
                }
                else
                {
                    this.logger.LogInformation($"[ChangePassword] old password cannot be empty");
                    await context.RespondAsync(ResponseWrapper<ChangePasswordResponse>.Fail("old password cannot be empty"));
                }
             
            }
            catch (Exception ex)
            {
                this.logger.LogDebug(ex, $"[ChangePassword] - exception occored. stacktrace: {ex.StackTrace}");
                await context.RespondAsync(ResponseWrapper<ChangePasswordResponse>.Fail(ex.Message));
            }
        }
    }
}
