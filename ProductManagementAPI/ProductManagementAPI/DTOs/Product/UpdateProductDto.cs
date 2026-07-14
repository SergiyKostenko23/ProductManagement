namespace ProductManagementAPI.DTOs.Product;

/// <summary>
/// Update product data transfer object
/// </summary>
public class UpdateProductDto
{
    /// <summary>
    /// Name of the product
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Description of the product
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Category of the product
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Priece of the product
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Stock of the product
    /// </summary>
    public int Stock { get; set; }
}
