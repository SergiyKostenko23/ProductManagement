namespace ProductManagementAPI.DTOs.Dashboard;

/// <summary>
/// Low stock product data transfer object
/// </summary>
public sealed class LowStockProductDto
{
    /// <summary>
    /// Id of the product
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Product number of the product
    /// </summary>
    public int ProductNumber { get; init; }

    /// <summary>
    /// Name of the product
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Category of the product
    /// </summary>
    public string Category { get; init; } = string.Empty;

    /// <summary>
    /// Stock of the product
    /// </summary>
    public int Stock { get; init; }
}