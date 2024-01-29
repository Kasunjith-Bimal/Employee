using Employee.Domain.Entities;
using Employee.Domain.Intefaces;
using Employee.Domain.Intefaces.IEmployeeRepository;
using Employee.Domain.Services;
using Employee.Infrastructure.Persistence.EFCore;
using Employee.Infrastructure.Persistence.Repository.EmployeeRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Employee.API.Configuration
{
    public static class EmployeeConfiguration
    {
        public static async void EmployeeServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddDbContext<EmployeeDbContext>(
                options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                }
           );

            // For Identity  
            services.AddIdentity<EmployeeDetail, IdentityRole>()
                            .AddEntityFrameworkStores<EmployeeDbContext>()
                            .AddDefaultTokenProviders();


            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                 // Adding Jwt Bearer  
                 .AddJwtBearer(options =>
                  {
                      options.SaveToken = true;
                      options.RequireHttpsMetadata = false;
                      options.TokenValidationParameters = new TokenValidationParameters()
                      {
                          ValidateIssuer = true,
                          ValidateAudience = true,
                          ValidAudience = configuration["JWT:ValidAudience"],
                          ValidIssuer = configuration["JWT:ValidIssuer"],
                          ClockSkew = TimeSpan.Zero,
                          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                      };
                  });


            //application Serrvice
            services.AddScoped<IEmployeeService, EmployeeService>();

            //application repository
            services.AddScoped<IEmployeeReadRepository, EmployeeReadRepository>();
            services.AddScoped<IEmployeeWriteRepository, EmployeeWriteRepository>();
        }

    }
}
