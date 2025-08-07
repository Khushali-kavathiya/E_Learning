using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Authentication;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.WebAPI.Middlewares;

/// <summary>
/// Middleware for globally handling unhandled exceptions in the application.
/// Logs the error and returns a JSON-formatted error response with appropriate status codes.
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _environment;

    // Exception-to-status code map
    private static readonly Dictionary<Type, (int StatusCode, string Message)> ExceptionStatusMap = new()
    {
        // Client-side exceptions
        {typeof(ArgumentNullException), (StatusCodes.Status400BadRequest, "Required input is missing.") },
        {typeof(ArgumentException), (StatusCodes.Status400BadRequest, "Invalid argument provided.")},
        {typeof(ValidationException), (StatusCodes.Status400BadRequest, "Validation failed.") },
        {typeof(UnauthorizedAccessException), (StatusCodes.Status401Unauthorized, "Access denied.") },
        {typeof(AuthenticationException), (StatusCodes.Status401Unauthorized, "Authentication failed.") },
        {typeof(KeyNotFoundException), (StatusCodes.Status404NotFound, "Resource not found.") },

        // Server-side exceptions
        {typeof(InvalidOperationException), (StatusCodes.Status500InternalServerError, "Invalid opearation.")},
        {typeof(DbUpdateException), (StatusCodes.Status500InternalServerError, "Database update failed.") },
        {typeof(DataException), (StatusCodes.Status500InternalServerError, "A data error occurred.")},
        {typeof(NullReferenceException), (StatusCodes.Status500InternalServerError, "Object reference not set.") },
        {typeof(NotSupportedException), (StatusCodes.Status501NotImplemented, "Operation not supported.") },
        {typeof(TimeoutException), (StatusCodes.Status504GatewayTimeout, "The request timed out.") },
        {typeof(Exception), (StatusCodes.Status500InternalServerError, "An unexpected error occurred.") }
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the request pipeline.</param>
    /// <param name="logger">Logger for recording exception details.</param>
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    /// <summary>
    /// Invokes the middleware to catch and handle exceptions that occur during request processing.
    /// </summary>
    /// <param name="context">The current HTTP context.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task InvokeAsync(HttpContext context)
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

    /// <summary>
    /// Handles the exception and writes a JSON-formatted error response to the response stream.
    /// </summary>
    /// <param name="context">The current HTTP context.</param>
    /// <param name="ex">The exception that occurred.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        _logger.LogError(ex, "An unhandled exception occurred");

        var exceptionType = ex.GetType();
        var (statusCode, message) = ExceptionStatusMap[typeof(Exception)];

        while (exceptionType != null)
        {
            if (ExceptionStatusMap.TryGetValue(exceptionType, out var mapping))
            {
                statusCode = mapping.StatusCode;
                message = mapping.Message;
                break;
            }
            exceptionType = exceptionType.BaseType;
        }

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        // Include additional details for debugging purposes
        var errorResponse = new
        {
            message,
            error = _environment.IsDevelopment() ? ex.Message : null,
            stackTrace = _environment.IsDevelopment() ? ex.StackTrace : null
        };

        var json = JsonSerializer.Serialize(errorResponse);
        await context.Response.WriteAsync(json);

    }
}