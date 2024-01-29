using Employee.Application.Command.EmployeeReleted.UpdateEmployeeByAdmin;
using Employee.Application.Queries.EmployeeReleted.GetAllAdmin;
using Employee.Application.Queries.EmployeeReleted.GetAllEmployee;
using Employee.Application.Queries.EmployeeReleted.GetEmployeeById;
using Employee.Application.Wrappers;
using Employee.Domain.Entities;
using MassTransit.Mediator;
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

        [HttpPut("employee/{id}")]
        public async Task<IActionResult> UpdateEmployee(string id, EmployeeDetail employee)
        {
            try
            {
                //if (id != task.Id)
                //    return BadRequest();

                var client = this.mediator.CreateRequestClient<UpdateEmployeeByAdminCommand>();
                var response = await client.GetResponse<ResponseWrapper<UpdateEmployeeByAdminResponse>>(new UpdateEmployeeByAdminCommand
                {
                    employee = employee,
                    Id = id
                });

                if (response.Message.Succeeded)
                {
                    this.logger.LogInformation($"Event succeeded in TaskController:UpdateTask");
                    return Ok(response.Message);
                }
                else
                {
                    this.logger.LogInformation($"Event not succeeded in TaskController:UpdateTask. Message: {response.Message.Message}");
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {

                this.logger.LogInformation($"Exception occurred in TaskController:UpdateTask. Message: {ex.Message} - Exception: {ex.InnerException?.ToString()} - StackTrace: {ex.StackTrace}");
                return BadRequest(ex);
            }
        }
    }
}
