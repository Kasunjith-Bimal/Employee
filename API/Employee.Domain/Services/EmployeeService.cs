using Employee.Domain.Entities;
using Employee.Domain.Enum;
using Employee.Domain.Intefaces;
using Employee.Domain.Intefaces.IEmployeeRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using SendGrid;
using SendGrid.Helpers.Mail;
using MimeKit;
using MailKit.Net.Smtp;
using System.Reflection.Metadata;

namespace Employee.Domain.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeReadRepository _readRepository;
        private readonly IEmployeeWriteRepository _writeRepository;
        private readonly ILogger<EmployeeService> logger;
        private readonly IConfiguration _configuration;
        public EmployeeService(IEmployeeReadRepository readRepository, IEmployeeWriteRepository writeRepository, ILogger<EmployeeService> logger, IConfiguration configuration)
        {
            this._readRepository = readRepository;
            this._writeRepository = writeRepository;
            this.logger = logger;
            this._configuration = configuration;
        }

        public async Task<EmployeeDetail> GetEmployeeByIdAsync(string employeeId)
        {
            try
            {
                this.logger.LogInformation($"[EmployeeService:GetEmployeeByIdAsync] employeeId : {employeeId} recieved event");
                return await _readRepository.GetEmployeByIdAsync(employeeId);
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[EmployeeService:GetEmployeeByIdAsync] employeeId {employeeId} exception occurred: {ex.Message} - Stacktrace: {ex.StackTrace}");
                return null;
            }
          
        }

        public async Task<List<EmployeeDetail>> GetAllEmployeesAsync()
        {
            try
            {
                this.logger.LogInformation($"[EmployeeService:GetAllEmployeesAsync] recieved event");
                return await _readRepository.GetAllEmployesAsync();
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[EmployeeService:GetAllEmployeesAsync] exception occurred: {ex.Message} - Stacktrace: {ex.StackTrace}");
                return null;

            }
        }

        public async Task<EmployeeDetail> AddEmployee(EmployeeDetail employee,RoleType roleType)
        {
            try
            {
                this.logger.LogInformation($"[EmployeeService:AddEmployee] recieved event employee email : {employee.Email}");
                var generateTemporyPassword = GenerateTemporaryPassword(10);
                this.logger.LogInformation($"[EmployeeService:AddEmployee] recieved event employee email : {employee.Email} tempory password :{generateTemporyPassword}");
                var employeeDetails =  await _writeRepository.AddEmployee(employee, generateTemporyPassword, roleType);

                if (employeeDetails != null)
                {
                    this.logger.LogInformation($"[EmployeeService:AddEmployee] send email : {employee.Email} to temporypassword : {generateTemporyPassword}");
                    if (string.IsNullOrEmpty(generateTemporyPassword))
                    {
                        await this.SendEmailAsync(employeeDetails.Email, "Your temporary password", $"Your temporary password is: {generateTemporyPassword}", employee.FullName);
                    }
                    return employeeDetails;
                }
                else
                {
                    this.logger.LogInformation($"[EmployeeService:AddEmployee] fail to add email : {employee.Email} to temporypassword : {generateTemporyPassword}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[EmployeeService:AddEmployee] exception occurred: {ex.Message} - Stacktrace: {ex.StackTrace}");
                return null;
                
            }
           
        }

        private string GenerateTemporaryPassword(int length = 10)
        {
            try
            {
                this.logger.LogInformation($"[EmployeeService:GenerateTemporaryPassword] recieved event");
                const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
                const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                const string numberChars = "0123456789";
                const string specialChars = "!@#$%^&*";

                Random random = new Random();

                // Ensure the password contains at least one character of each type
                var password = new char[length];
                password[0] = lowerChars[random.Next(lowerChars.Length)];
                password[1] = upperChars[random.Next(upperChars.Length)];
                password[2] = numberChars[random.Next(numberChars.Length)];
                password[3] = specialChars[random.Next(specialChars.Length)];

                // Fill the rest of the password with random characters from all types
                string allChars = lowerChars + upperChars + numberChars + specialChars;
                for (int i = 4; i < length; i++)
                {
                    password[i] = allChars[random.Next(allChars.Length)];
                }

                this.logger.LogInformation($"[EmployeeService:GenerateTemporaryPassword] Success event");
                // Shuffle the password to randomize character positions
                return new string(password.OrderBy(x => random.Next()).ToArray());

            }
            catch (Exception ex)
            {

                return "";
            }
            
        }

        public async Task<EmployeeDetail> UpdateEmployee(EmployeeDetail employee)
        {
            try
            {
                this.logger.LogInformation($"[EmployeeService:UpdateEmployee] recieved event employee id: {employee.Id} email : {employee.Email}");
                return await _writeRepository.UpdateEmployee(employee);
            }
            catch (Exception ex)
            {

                this.logger.LogDebug($"[EmployeeService:UpdateEmployee] employee id: {employee.Id} exception occurred: {ex.Message} - Stacktrace: {ex.StackTrace}");
                return null;
            }

        }

        public async Task<bool> DeleteEmployee(EmployeeDetail employee)
        {
            try
            {

                this.logger.LogInformation($"[EmployeeService:DeleteEmployee] recieved event employee id: {employee.Id}");
                return await _writeRepository.DeleteEmployee(employee);
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[EmployeeService:DeleteEmployee] employee id: {employee.Id} exception occurred: {ex.Message} - Stacktrace: {ex.StackTrace}");
                return await Task.FromResult(false);
            }
            
        }


        public async Task SendEmailAsync(string email, string subject, string message, string fullName)
        {
            try
            {
                this.logger.LogInformation($"[EmployeeService:SendEmailAsync] send  email : {email} message :{message}");

                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress(_configuration["EmailSettings:SenderName"], _configuration["EmailSettings:Sender"]));
                emailMessage.To.Add(new MailboxAddress(fullName, email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    // Gmail SMTP server address
                    await client.ConnectAsync(_configuration["EmailSettings:MailServer"], Convert.ToInt32(_configuration["EmailSettings:MailPort"]), false);
                    await client.AuthenticateAsync(_configuration["EmailSettings:Sender"], _configuration["EmailSettings:Password"]);
                    await client.SendAsync(emailMessage);

                    await client.DisconnectAsync(true);

                    this.logger.LogInformation($"[EmployeeService:SendEmailAsync] recieved  email : {email} message :{message}");
                }

            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[EmployeeService:DeleteEmployee] employee email: {email} exception occurred: {ex.Message} - Stacktrace: {ex.StackTrace}");
            }
        }

        public async Task<EmployeeDetail> GetEmployeeByEmailAsync(string email)
        {
            try
            {
                this.logger.LogInformation($"[EmployeeService:GetEmployeeByEmailAsync] email : {email} recieved event");
                return await _readRepository.GetEmployeByEmailAsync(email);
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[EmployeeService:GetEmployeeByEmailAsync] email {email} exception occurred: {ex.Message} - Stacktrace: {ex.StackTrace}");
                return null;
            }
        }

        public async Task<List<EmployeeDetail>> GetAllAdminsAsync()
        {
            try
            {
                this.logger.LogInformation($"[EmployeeService:GetAllAdminsAsync] recieved event");
                return await _readRepository.GetAllAdminsAsync();
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[EmployeeService:GetAllAdminsAsync] exception occurred: {ex.Message} - Stacktrace: {ex.StackTrace}");
                return null;

            }
        }

        public async Task<bool> CheckPasswordAsync(EmployeeDetail employee, string passWord)
        {
            try
            {
                this.logger.LogInformation($"[EmployeeService:CheckPasswordAsync] recieved event");
                return await _readRepository.CheckPasswordAsync(employee, passWord);
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[EmployeeService:CheckPasswordAsync] exception occurred: {ex.Message} - Stacktrace: {ex.StackTrace}");
                return await Task.FromResult(false);

            }
        }

        public async Task<IList<string>> GetUserRolesAsync(EmployeeDetail employee)
        {
            try
            {
                return await _readRepository.GetUserRolesAsync(employee);
            }
            catch (Exception ex)
            {

                this.logger.LogDebug($"[EmployeeService:CheckPasswordAsync] exception occurred: {ex.Message} - Stacktrace: {ex.StackTrace}");
                return null;

            }
        }
    }
}
