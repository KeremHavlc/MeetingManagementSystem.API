using System.Net;
using System.Text.Json;
using MeetingManagementSystem.Domain.Dtos;
using Serilog;

namespace MeetingManagementSystem.WebAPI.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var traceId = Guid.NewGuid().ToString();
                Log.Error(ex, "Unhandled exception. TraceId: {TraceId}, Path: {Path}", traceId, context.Request.Path);

                await HandleExceptionAsync(context, ex, traceId);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex, string traceId)
        {
            context.Response.ContentType = "application/json";

            var response = new MessageResponse
            {
                Success = false,
                Data = null,
                Message = $"Beklenmeyen bir hata oluştu. TraceId: {traceId}"
            };

            if (ex is FluentValidation.ValidationException vex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = string.Join(" | ", vex.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}"));
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}
