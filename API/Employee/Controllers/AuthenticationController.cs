using Employee.API.Dtos;
using Employee.Application.Command.AuthenticationReleted.ChangePassword;
using Employee.Application.Command.AuthenticationReleted.Login;
using Employee.Application.Command.AuthenticationReleted.Register;
using Employee.Application.Wrappers;
using Employee.Domain.Enum;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<AuthenticationController> logger;

        public AuthenticationController(IMediator mediator, ILogger<AuthenticationController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterEmployee(RegisterDto register)
        {
            try
            {
                var client = this.mediator.CreateRequestClient<RegisterEmployeeCommand>();
                var response = await client.GetResponse<ResponseWrapper<RegisterEmployeeResponse>>(new RegisterEmployeeCommand
                {
                    Address = String.IsNullOrEmpty(register.Address) ? "" : register.Address,
                    Email = String.IsNullOrEmpty(register.Email) ? "" : register.Email,
                    RoleType = register.RoleType,
                    FirstName = String.IsNullOrEmpty(register.FirstName) ? "" : register.FirstName,
                    LastName = String.IsNullOrEmpty(register.LastName) ? "" : register.LastName,
                    JoinDate = register.JoinDate,
                    Salary = register.Salary,
                    Telephone = register.Telephone,
                });

                if (response.Message.Succeeded)
                {
                    this.logger.LogInformation($"Event succeeded in AuthenticationController:RegisterEmployee");
                    return Ok(response.Message);
                   // return CreatedAtAction(nameof(GetTaskById), new { id = response.Message.Payload.task.Id }, response.Message);
                }
                else
                {
                    this.logger.LogInformation($"Event not succeeded in AuthenticationController:RegisterEmployee. Message: {response.Message.Message}");
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"Exception occurred in TaskController:RegisterEmployee. Message: {ex.Message} - Exception: {ex.InnerException?.ToString()} - StackTrace: {ex.StackTrace}");
                return BadRequest(ex);
            }
        }


        [HttpPost("Login")]
        public async Task<IActionResult> LoginEmployee(LoginDto login)
        {
            try
            {
                var client = this.mediator.CreateRequestClient<LoginEmployeeCommand>();
                var response = await client.GetResponse<ResponseWrapper<LoginEmployeeResponse>>(new LoginEmployeeCommand
                {
                    Email = login.Email,
                    Password = login.Password
                });
                

                if (response.Message.Succeeded)
                {
                    this.logger.LogInformation($"Event succeeded in AuthenticationController:RegisterEmployee");
                    return Ok(response.Message);
                    // return CreatedAtAction(nameof(GetTaskById), new { id = response.Message.Payload.task.Id }, response.Message);
                }
                else
                {
                    this.logger.LogInformation($"Event not succeeded in AuthenticationController:RegisterEmployee. Message: {response.Message.Message}");
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"Exception occurred in TaskController:RegisterEmployee. Message: {ex.Message} - Exception: {ex.InnerException?.ToString()} - StackTrace: {ex.StackTrace}");
                return BadRequest(ex);
            }
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            try
            {
                var client = this.mediator.CreateRequestClient<ChangePasswordCommand>();
                var response = await client.GetResponse<ResponseWrapper<ChangePasswordResponse>>(new ChangePasswordCommand
                {
                    Email = changePasswordDto.Email,
                    OldPassword = changePasswordDto.OldPassword,
                    NewPassword = changePasswordDto.NewPassword
                });


                if (response.Message.Succeeded)
                {
                    this.logger.LogInformation($"Event succeeded in AuthenticationController:ChangePassword");
                    return Ok(response.Message);
                    // return CreatedAtAction(nameof(GetTaskById), new { id = response.Message.Payload.task.Id }, response.Message);
                }
                else
                {
                    this.logger.LogInformation($"Event not succeeded in AuthenticationController:ChangePassword. Message: {response.Message.Message}");
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"Exception occurred in TaskController:ChangePassword. Message: {ex.Message} - Exception: {ex.InnerException?.ToString()} - StackTrace: {ex.StackTrace}");
                return BadRequest(ex);
            }
        }
    }
}
