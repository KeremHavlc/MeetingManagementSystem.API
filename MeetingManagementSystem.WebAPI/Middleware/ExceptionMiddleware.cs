using System.Net;
using System.Text.Json;
using MeetingManagementSystem.Domain.Dtos;

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
                _logger.LogError(ex, "Bir hata yakalandı!");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var response = new MessageResponse
            {
                Success = false,
                Data = null
            };

            // FluentValidation hatası mı?
            if (ex is FluentValidation.ValidationException vex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = string.Join(" | ", vex.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}"));
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = $"Beklenmeyen bir hata oluştu: {ex.Message}";
            }

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}
