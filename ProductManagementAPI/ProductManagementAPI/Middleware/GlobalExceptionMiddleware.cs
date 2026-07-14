using ProductManagementAPI.Common;
using ProductManagementAPI.Services;
using System.Text.Json;

namespace ProductManagementAPI.Middleware;

/// <summary>
/// Global exception handling middleware
/// </summary>
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<ProductService> _logger;

    /// <summary>
    /// Global exception handling middleware constructor
    /// </summary>
    /// <param name="next">Request delegate</param>
    /// <param name="environment"> Environment</param>
    /// <param name="logger"> Logger</param>
    public GlobalExceptionMiddleware(
        RequestDelegate next,
        IWebHostEnvironment environment,
        ILogger<ProductService> logger)
    {
        _next        = next;
        _environment = environment;
        _logger      = logger;
    }

    /// <summary>
    /// Is invoked for each HTTP request to handle exceptions globally
    /// </summary>
    /// <param name="context">HTTP context</param>
    /// <returns>Task</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    /// <summary>
    /// Handles exceptions and returns a standardized error response
    /// </summary>
    /// <param name="context">HTTP context</param>
    /// <param name="exception">Exception</param>
    /// <returns>Task</returns>
    private async Task HandleException(
    HttpContext context,
    Exception exception)
    {
        _logger.LogError(
            exception,
            "Unhandled exception while processing request.");

        var statusCode = GetStatusCode(exception);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode  = statusCode;

        var response = new ErrorResponse
        {
            StatusCode = statusCode,
            Message    = "An unexpected error occurred.",
            Details    = _environment.IsDevelopment()
                ? exception.Message
                : null
        };

        var json = JsonSerializer.Serialize(response);

        await context.Response.WriteAsync(json);
    }

    /// <summary>
    /// Maps exceptions to appropriate HTTP status codes
    /// </summary>
    /// <param name="ex">Exception</param>
    /// <returns>Status code</returns>
    private static int GetStatusCode(Exception ex)
    {
        return ex switch
        {
            ArgumentException           => StatusCodes.Status400BadRequest,
            KeyNotFoundException        => StatusCodes.Status404NotFound,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,

            _ => StatusCodes.Status500InternalServerError
        };
    }
}