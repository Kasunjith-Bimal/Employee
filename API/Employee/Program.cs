
using Employee.API.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Serilog;
using System;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Application Start");
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    var allowedHosts = builder.Configuration.GetValue(typeof(string), "AllowedHosts") as string;

    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(
            builder =>
            {
                if (allowedHosts == null || allowedHosts == "*")
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    return;
                }
                string[] hosts;
                if (allowedHosts.Contains(';'))
                    hosts = allowedHosts.Split(';');
                else
                {
                    hosts = new string[1];
                    hosts[0] = allowedHosts;
                }
                builder.WithOrigins(hosts)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            });
    });

    builder.Services.EmployeeServices(builder.Configuration, builder.Environment);
    // Add services to the container.
 
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

        // Add security definition for JWT
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });
    });


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseCors(x => x
           .AllowAnyMethod()
           .AllowAnyHeader()
           .SetIsOriginAllowed(origin => true)
           .AllowCredentials());

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{

    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
