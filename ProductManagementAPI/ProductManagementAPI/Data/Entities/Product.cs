using System.ComponentModel.DataAnnotations;

namespace ProductManagementAPI.Data.Entities;

/// <summary>
/// Product entity
/// </summary>
public class Product
{
    /// <summary>
    /// Id of the product
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Product number of the product
    /// </summary>
    [Required]
    public int ProductNumber { get; set; }

    /// <summary>
    /// Name of the product
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = "";

    /// <summary>
    /// Description of the product
    /// </summary>
    [MaxLength(500)]
    public string? Description { get; set; } = "";

    /// <summary>
    /// Price of the product
    /// </summary>
    public decimal Price { get; set; } = decimal.Zero;

    /// <summary>
    /// Stock of the product
    /// </summary>
    public int Stock { get; set; } = 0;

    /// <summary>
    /// Category of the product
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Creation date of the product
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Update date of the product
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}
