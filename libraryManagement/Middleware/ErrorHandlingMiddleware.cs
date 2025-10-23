using System.Text.Json;
using libraryManagement.Models.Exceptions;
using libraryManagement.Models.Responses;

namespace libraryManagement.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly IWebHostEnvironment _environment;

    public ErrorHandlingMiddleware(
        RequestDelegate next,
        ILogger<ErrorHandlingMiddleware> logger,
        IWebHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Произошла непредвиденная ошибка");
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, message, details) = GetExceptionDetails(exception);

        var response = new ErrorResponse
        {
            StatusCode = statusCode,
            Message = message,
            Details = details,
            Timestamp = DateTime.UtcNow,
            RequestId = context.TraceIdentifier
        };

        if (_environment.IsDevelopment())
            response.StackTrace = exception.StackTrace;

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = _environment.IsDevelopment()
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
    }

    private (int statusCode, string message, string details) GetExceptionDetails(Exception exception)
    {
        return exception switch
        {
            NotFoundException ex =>
                (StatusCodes.Status404NotFound, "Запись не найдена", ex.Message),

            BadRequestException ex =>
                (StatusCodes.Status400BadRequest, "Некорректный запрос", ex.Message),
            
            ArgumentException ex =>
                (StatusCodes.Status404NotFound, "Запись не существует", ex.Message)
        };
    }
}