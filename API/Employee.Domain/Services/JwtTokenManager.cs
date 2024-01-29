using Employee.Domain.Entities;
using Employee.Domain.Intefaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Employee.Domain.Services
{
    public class JwtTokenManager : IJwtTokenManager
    {
        private readonly IConfiguration configuration;
        private readonly IEmployeeService employeeService;
        public JwtTokenManager(IConfiguration configuration, IEmployeeService employeeService)
        {
            this.configuration = configuration;
            this.employeeService = employeeService;
        }
        public async Task<Tuple<string, DateTime>> GenerateToken(EmployeeDetail employee)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.configuration["JWT:Secret"]);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, employee.Id),
                new Claim(ClaimTypes.Email, employee.Email),
                new Claim(ClaimTypes.Name, employee.FullName)
                        // Add other claims as needed
            };

            var roles = await employeeService.GetUserRolesAsync(employee);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            DateTime expiry = DateTime.UtcNow.AddDays(7);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiry,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = this.configuration["JWT:ValidIssuer"],
                Audience = this.configuration["JWT:ValidAudience"],
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string exportedToken = tokenHandler.WriteToken(token);

            Tuple<string, DateTime> output = new Tuple<string, DateTime>(exportedToken, expiry);

            return await Task.FromResult<Tuple<string, DateTime>>(output);
        }
    }
}
