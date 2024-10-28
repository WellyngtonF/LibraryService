using System.Net;
using System.Text.Json;
using Api.Exceptions;
using Api.Entities.Models;

namespace Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                
                context.Response.ContentType = "application/json";
                
                var response = _env.IsDevelopment()
                    ? new ErrorResponse(ex.Message, GetStatusCode(ex), ex.StackTrace)
                    : new ErrorResponse(ex.Message, GetStatusCode(ex));

                context.Response.StatusCode = response.StatusCode;

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }

        private static int GetStatusCode(Exception exception)
        {
            return exception switch
            {
                ApiException apiException => apiException.StatusCode,
                _ => (int)HttpStatusCode.InternalServerError
            };
        }
    }
}
