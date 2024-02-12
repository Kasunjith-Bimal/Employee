using Employee.API.Dtos;
using Employee.Application.Command.EmployeeReleted.DeleteEmployee;
using Employee.Application.Command.EmployeeReleted.UpdateEmployeeByAdmin;
using Employee.Application.Queries.EmployeeReleted.GetAllAdmin;
using Employee.Application.Queries.EmployeeReleted.GetAllEmployee;
using Employee.Application.Queries.EmployeeReleted.GetEmployeeById;
using Employee.Application.Wrappers;
using Employee.Domain.Entities;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IMediator mediator;
        private readonly ILogger<AdminController> logger;

        public AdminController(IMediator mediator, ILogger<AdminController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

      
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAdminEmployee()
        {
            try
            {
                var client = this.mediator.CreateRequestClient<GetAllAdminQuery>();
                var response = await client.GetResponse<ResponseWrapper<GetAllAdminResponse>>(new GetAllAdminQuery
                {

                });

                if (response.Message.Succeeded)
                {
                    this.logger.LogInformation($"Event succeeded in AdminController:GetAllAdminEmployee");
                    return Ok(response.Message);
                    // return CreatedAtAction(nameof(GetTaskById), new { id = response.Message.Payload.task.Id }, response.Message);
                }
                else
                {
                    this.logger.LogInformation($"Event not succeeded in AdminController:GetAllAdminEmployee. Message: {response.Message.Message}");
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"Exception occurred in AdminController:GetAllAdminEmployee. Message: {ex.Message} - Exception: {ex.InnerException?.ToString()} - StackTrace: {ex.StackTrace}");
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEmployeeByid(string id)
        {
            try
            {
                var client = this.mediator.CreateRequestClient<GetEmployeeByIdQuery>();
                var response = await client.GetResponse<ResponseWrapper<GetEmployeeByIdResponse>>(new GetEmployeeByIdQuery
                {
                    Id = string.IsNullOrEmpty(id) ? "" : id
                });

                if (response.Message.Succeeded)
                {
                    this.logger.LogInformation($"Event succeeded in AdminController:GetAllEmployee");
                    return Ok(response.Message);
                    // return CreatedAtAction(nameof(GetTaskById), new { id = response.Message.Payload.task.Id }, response.Message);
                }
                else
                {
                    this.logger.LogInformation($"Event not succeeded in AdminController:GetAllEmployee. Message: {response.Message.Message}");
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"Exception occurred in AdminController:GetAllEmployee. Message: {ex.Message} - Exception: {ex.InnerException?.ToString()} - StackTrace: {ex.StackTrace}");
                return BadRequest(ex);
            }
        }

        [HttpPut("employee/{id}/edit")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEmployee(string id, EditDto employee)
        {
            try
            {
                //if (id != task.Id)
                //    return BadRequest();

                var client = this.mediator.CreateRequestClient<UpdateEmployeeByAdminCommand>();
                var response = await client.GetResponse<ResponseWrapper<UpdateEmployeeByAdminResponse>>(new UpdateEmployeeByAdminCommand
                {
                    Employee = new UpdateEmployeeByAdminDetails
                    {
                        Id = employee.Id,
                        Email = employee.Email,
                        Address = employee.Address,
                        FullName = employee.FullName,
                        JoinDate = employee.JoinDate,
                        RoleType = employee.RoleType,
                        Salary = employee.Salary,
                        Telephone = employee.Telephone,
                        IsActive = employee.IsActive

                    },
                    Id = id
                });

                if (response.Message.Succeeded)
                {
                    this.logger.LogInformation($"Event succeeded in AdminController:UpdateEmployee");
                    return Ok(response.Message);
                }
                else
                {
                    this.logger.LogInformation($"Event not succeeded in AdminController:UpdateEmployee. Message: {response.Message.Message}");
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {

                this.logger.LogInformation($"Exception occurred in AdminController:UpdateEmployee. Message: {ex.Message} - Exception: {ex.InnerException?.ToString()} - StackTrace: {ex.StackTrace}");
                return BadRequest(ex);
            }
        }

        [HttpDelete("employee/{id}/delete")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteEmployee(string id)
        {
            try
            {
                var client = this.mediator.CreateRequestClient<DeleteEmployeeCommand>();
                var response = await client.GetResponse<ResponseWrapper<DeleteEmployeeResponse>>(new DeleteEmployeeCommand
                {
                    Id = id
                });

                if (response.Message.Succeeded)
                {
                    this.logger.LogInformation($"Event succeeded in AdminController:DeleteEmployee");
                    return Ok(response.Message);
                }
                else
                {
                    this.logger.LogInformation($"Event not succeeded in AdminController:DeleteEmployee. Message: {response.Message.Message}");
                    return NotFound(response.Message);

                }
            }
            catch (Exception ex)
            {

                this.logger.LogInformation($"Exception occurred in AdminController:UpdateTask. Message: {ex.Message} - Exception: {ex.InnerException?.ToString()} - StackTrace: {ex.StackTrace}");
                return BadRequest(ex);
            }
        }


    }
}
