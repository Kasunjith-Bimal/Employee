using MassTransit;

namespace Employee.API.Filters
{
    public class HttpContextScopeFilter : IFilter<PublishContext>, IFilter<SendContext>, IFilter<ConsumeContext>
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public HttpContextScopeFilter(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        private void AddPayload(PipeContext context)
        {
            if (this.httpContextAccessor.HttpContext == null)
            {
                return;
            }

            var serviceProvider = this.httpContextAccessor.HttpContext.RequestServices;
            context.GetOrAddPayload(() => serviceProvider);
            context.GetOrAddPayload<IServiceScope>(() => new NoopScope(serviceProvider));
        }

        public Task Send(PublishContext context, IPipe<PublishContext> next)
        {
            AddPayload(context);
            return next.Send(context);
        }

        public Task Send(SendContext context, IPipe<SendContext> next)
        {
            AddPayload(context);
            return next.Send(context);
        }

        public Task Send(ConsumeContext context, IPipe<ConsumeContext> next)
        {
            AddPayload(context);
            return next.Send(context);
        }

        public void Probe(ProbeContext context)
        {

        }

        private class NoopScope : IServiceScope
        {
            public NoopScope(IServiceProvider serviceProvider)
            {
                ServiceProvider = serviceProvider;
            }

            public void Dispose()
            {

            }

            public IServiceProvider ServiceProvider { get; }

        }
    }
}
