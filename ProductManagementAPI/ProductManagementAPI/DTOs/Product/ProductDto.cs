namespace ProductManagementAPI.DTOs.Product;

/// <summary>
/// Product data transfer object
/// </summary>
public class ProductDto
{
    /// <summary>
    /// Id of the product
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Product number of the product
    /// </summary>
    public int ProductNumber { get; set; }

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
    /// Price   
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Stock of the product
    /// </summary>
    public int Stock { get; set; }
}