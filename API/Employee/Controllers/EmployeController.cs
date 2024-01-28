﻿using Employee.API.Dtos;
using Employee.Application.Command.AuthenticationReleted.Register;
using Employee.Application.Queries.EmployeeReleted.GetAllEmployee;
using Employee.Application.Queries.EmployeeReleted.GetEmployeeById;
using Employee.Application.Wrappers;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<EmployeController> logger;

        public EmployeController(IMediator mediator, ILogger<EmployeController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            try
            {
                var client = this.mediator.CreateRequestClient<GetAllEmployeeQuery>();
                var response = await client.GetResponse<ResponseWrapper<GetAllEmployeeResponse>>(new GetAllEmployeeQuery
                {
                  
                });

                if (response.Message.Succeeded)
                {
                    this.logger.LogInformation($"Event succeeded in EmployeController:GetAllEmployee");
                    return Ok(response.Message);
                    // return CreatedAtAction(nameof(GetTaskById), new { id = response.Message.Payload.task.Id }, response.Message);
                }
                else
                {
                    this.logger.LogInformation($"Event not succeeded in EmployeController:GetAllEmployee. Message: {response.Message.Message}");
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"Exception occurred in EmployeController:GetAllEmployee. Message: {ex.Message} - Exception: {ex.InnerException?.ToString()} - StackTrace: {ex.StackTrace}");
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
                    this.logger.LogInformation($"Event succeeded in EmployeController:GetAllEmployee");
                    return Ok(response.Message);
                    // return CreatedAtAction(nameof(GetTaskById), new { id = response.Message.Payload.task.Id }, response.Message);
                }
                else
                {
                    this.logger.LogInformation($"Event not succeeded in EmployeController:GetAllEmployee. Message: {response.Message.Message}");
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"Exception occurred in EmployeController:GetAllEmployee. Message: {ex.Message} - Exception: {ex.InnerException?.ToString()} - StackTrace: {ex.StackTrace}");
                return BadRequest(ex);
            }
        }
    }
}
