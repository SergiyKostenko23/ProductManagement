namespace ProductManagementAPI.Common.Enums;

/// <summary>
/// Stock update result enumeration
/// </summary>
public enum StockUpdateResult
{
    /// <summary>
    /// Stock updated successfully
    /// </summary>
    Success,
    /// <summary>
    /// Product not found
    /// </summary>
    ProductNotFound,
    /// <summary>
    /// Insufficient stock to decrement
    /// </summary>
    InsufficientStock
}
