using System.Text.Json;
using UrphaCapital.Application.ExternalServices.ErrorSender;

namespace UrphaCapital.API.Middlewares
{
    public class GlobalExceptionHandler
    {
        public RequestDelegate _next;
        public ILogger<GlobalExceptionHandler> _logger;
        private readonly IErrorSenderService _errorSenderService;
        public GlobalExceptionHandler(RequestDelegate requestDelegate, ILogger<GlobalExceptionHandler> logger, IErrorSenderService errorSenderService)
        {
            _next = requestDelegate;
            _logger = logger;
            _errorSenderService = errorSenderService;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex.Message);
            await _errorSenderService.SendError(ex.Message);

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var result = JsonSerializer.Serialize(new { error = ex.Message });
            await context.Response.WriteAsync(result);
        }
    }
}
