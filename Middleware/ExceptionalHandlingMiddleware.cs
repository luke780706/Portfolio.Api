using System.Net;
using System.Text.Json;
using Portfolio.Api.Models;

namespace Portfolio.Api.Middleware
{
    public class ExceptionalHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionalHandlingMiddleware> _logger;

        public ExceptionalHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionalHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var errorResponse = new ErrorResponse
                {
                    Status = context.Response.StatusCode,
                    Message = "An unexpected error occurred."
                };

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(errorResponse));
            }
        }
    }
}