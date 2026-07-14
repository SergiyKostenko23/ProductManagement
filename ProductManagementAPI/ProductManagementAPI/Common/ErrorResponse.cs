namespace ProductManagementAPI.Common;

/// <summary>
/// Error response model
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// Status code of the error response
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Message describing the error
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Error details, if any
    /// </summary>
    public string? Details { get; set; }
}
