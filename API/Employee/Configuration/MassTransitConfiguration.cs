using Employee.API.Extensions;
using Employee.Application.Command.AuthenticationReleted.Register;
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
                   
                    x.ConfigureMediator((context, cfg) => cfg.UseHttpContextScopeFilter(context));
                });

            }
    }
}
