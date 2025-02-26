using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;
using task_service.DTO;

namespace task_service.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            catch (
            Exception e)
            {
                await HandleExceptionAsync(httpContext, e.Message, HttpStatusCode.NotFound, "The user cannot be null1");
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, string exMsg, HttpStatusCode httpStatusCode, string message)
        {
            _logger.LogError(exMsg);

            HttpResponse response = httpContext.Response;

            response.ContentType  = "application/json";
            response.StatusCode = (int)httpStatusCode;

            ErrorDTO errorDTO = new ErrorDTO()
            {
                Message = message,
                StatusCode = (int)httpStatusCode
            };

            string result = errorDTO.ToString();

            await response.WriteAsJsonAsync(result);
        }
    }
}
