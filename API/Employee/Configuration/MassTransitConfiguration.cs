using Employee.API.Extensions;
using Employee.Application.Command.AuthenticationReleted.ChangePassword;
using Employee.Application.Command.AuthenticationReleted.Login;
using Employee.Application.Command.AuthenticationReleted.Register;
using Employee.Application.Command.EmployeeReleted.DeleteEmployee;
using Employee.Application.Command.EmployeeReleted.UpdateEmployeeByAdmin;
using Employee.Application.Command.EmployeeReleted.UpdateEmployeeByEmployee;
using Employee.Application.Queries.EmployeeReleted.GetAllAdmin;
using Employee.Application.Queries.EmployeeReleted.GetAllEmployee;
using Employee.Application.Queries.EmployeeReleted.GetEmployeeById;
using MassTransit;

namespace Employee.API.Configuration
{
    public static class MassTransitConfiguration
    {
            public static void AddMassTransitComponents(this IServiceCollection services, IConfiguration configuration)
            {
                services.AddMediator(x =>
                {   // commands
                    x.AddConsumer<RegisterEmployee>();
                    x.AddConsumer<UpdateEmployeeByAdmin>();
                    x.AddConsumer<DeleteEmployee>();
                    x.AddConsumer<UpdateEmployeeByEmployee>();
                    x.AddConsumer<LoginEmployee>();
                    x.AddConsumer<ChangePassword>();
                    //queries
                    x.AddConsumer<GetAllAdmin>();
                    x.AddConsumer<GetAllEmployee>();
                    x.AddConsumer<GetEmployeeById>();
                    x.ConfigureMediator((context, cfg) => cfg.UseHttpContextScopeFilter(context));
                });

            }
    }
}
