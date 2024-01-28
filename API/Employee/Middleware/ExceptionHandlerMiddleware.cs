using System.Net;

namespace Employee.API.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Caught in global exception handler");

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            HttpResponse response = httpContext.Response;
            response.ContentType = "application/json";

            string responseBody;

            switch (exception)
            {
                case UnauthorizedAccessException e:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    responseBody = e.Message;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    responseBody = "Something went wrong. Please try again later.";
                    break;
            }

            await response.WriteAsync(responseBody);
        }
    }
}
