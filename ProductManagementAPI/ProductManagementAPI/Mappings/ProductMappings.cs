using ProductManagementAPI.Data.Entities;
using ProductManagementAPI.DTOs.Dashboard;
using ProductManagementAPI.DTOs.Product;

namespace ProductManagementAPI.Mappings;

/// <summary>
/// Product mapping extensions
/// </summary>
public static class ProductMappings
{
    /// <summary>
    /// Transforms a product entity to a product data transfer object
    /// </summary>
    /// <param name="product">Product entity</param>
    /// <returns>Product data transfer object</returns>
    public static ProductDto ToDto(this Product product)
    {
        return new ProductDto
        {
            Id            = product.Id,
            ProductNumber = product.ProductNumber,
            Name          = product.Name,
            Description   = product.Description,
            Category      = product.Category,
            Price         = product.Price,
            Stock         = product.Stock
        };
    }

    /// <summary>
    /// Transforms a list of product entities to a list of product data transfer objects
    /// </summary>
    /// <param name="products">List of products</param>
    /// <returns>List of product data transfer objects</returns>
    public static IEnumerable<ProductDto> ToDtos(this IEnumerable<Product> products)
    {
        return products.Select(p => p.ToDto());
    }

    /// <summary>
    /// Transforms a product entity to a low stock product data transfer object
    /// </summary>
    /// <param name="product">Product entity</param>
    /// <returns>Low stock product data transfer object</returns>
    public static LowStockProductDto ToLowStockDto(this Product product)
    {
        return new LowStockProductDto
        {
            Id            = product.Id,
            ProductNumber = product.ProductNumber,
            Name          = product.Name,
            Category      = product.Category,
            Stock         = product.Stock
        };
    }
}