using Employee.API.Filters;
using MassTransit;

namespace Employee.API.Extensions
{
    public static class MediatorHttpContextScopeFilterExtensions
    {
        public static void UseHttpContextScopeFilter(this IMediatorConfigurator configurator,
        IServiceProvider serviceProvider)
        {
            var filter = new HttpContextScopeFilter(serviceProvider.GetRequiredService<IHttpContextAccessor>());

            configurator.ConfigurePublish(x => x.UseFilter(filter));
            configurator.ConfigureSend(x => x.UseFilter(filter));
            configurator.UseFilter(filter);
        }
    }
}
