using ProductManagementAPI.Common.Enums;
using ProductManagementAPI.Data.Entities;
using ProductManagementAPI.DTOs.Product;

namespace ProductManagementAPI.Interfaces;

/// <summary>
/// Product service interface
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Gets all products
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Product>> GetAllAsync();

    /// <summary>
    /// Gets a product by its ID
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>Obtained product</returns>
    Task<Product?> GetByIdAsync(int id);

    /// <summary>
    /// Creates a new product
    /// </summary>
    /// <param name="dto">Create product data transfer object</param>
    /// <returns>Created product</returns>
    Task<Product> CreateAsync(CreateProductDto dto);

    /// <summary>
    /// Updates an existing product
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="dto">Update product data transfer object</param>
    /// <returns>Updated product</returns>
    Task<Product?> UpdateAsync(int id, UpdateProductDto dto);

    /// <summary>
    /// Deletes a product by its ID
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>True if successful, false if not found</returns>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Adds stock to a product
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="quantity">Quantity to be added</param>
    /// <returns>Action result</returns>
    Task<(StockUpdateResult, Product?)> AddStockAsync(int id, int quantity);

    /// <summary>
    /// Decrements stock from a product
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="quantity">Quantity to be decremented</param>
    /// <returns>Action result</returns>
    Task<(StockUpdateResult, Product?)> DecrementStockAsync(int id, int quantity);

    /// <summary>
    /// Searches for products by name
    /// </summary>
    /// <param name="name">Product name</param>
    /// <returns>List of products that were found</returns>
    /// <exception cref="ArgumentException"></exception>
    Task<IEnumerable<Product>> SearchByNameAsync(string name);

    /// <summary>
    /// Searches for products by product number
    /// </summary>
    /// <param name="productNumber">Product number</param>
    /// <returns>List of products that were found</returns>
    /// <exception cref="ArgumentException"></exception>
    Task<Product?> GetByProductNumberAsync(int productNumber);

    /// <summary>
    /// Gets a list of products that are within stock range
    /// </summary>
    /// <param name="min">Minimum stock</param>
    /// <param name="max">Maximum stock</param>
    /// <returns>List of products that were found</returns>
    /// <exception cref="ArgumentException"></exception>
    Task<IEnumerable<Product>> GetByStockRangeAsync(int min, int max);
}