using ProductManagementAPI.Data.Entities;
using ProductManagementAPI.DTOs.Dashboard;

namespace ProductManagementAPI.Interfaces;

/// <summary>
/// Product repository interface
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Gets all products
    /// </summary>
    /// <returns>List of obtained products</returns>
    Task<IEnumerable<Product>> GetAllAsync();

    /// <summary>
    /// Gets a product by its ID
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>Obtained product</returns>
    Task<Product?> GetByIdAsync(int id);

    /// <summary>
    /// Gets a product by its product number
    /// </summary>
    /// <param name="productNumber">Product number</param>
    /// <returns>Obtained product</returns>
    Task<Product?> GetByProductNumberAsync(int productNumber);

    /// <summary>
    /// Searches for products by name
    /// </summary>
    /// <param name="name">Product name</param>
    /// <returns>List of products that were found</returns>
    Task<IEnumerable<Product>> SearchByNameAsync(string name);

    /// <summary>
    /// Gets a list of products that are within stock range
    /// </summary>
    /// <param name="min">Minimum stock</param>
    /// <param name="max">Maximum stock</param>
    /// <returns>List of products that were found</returns>
    Task<IEnumerable<Product>> GetByStockRangeAsync(int min, int max);

    /// <summary>
    /// Creates a new product
    /// </summary>
    /// <param name="product">Product to be created</param>
    Task AddAsync(Product product);

    /// <summary>
    /// Updates an existing product
    /// </summary>
    /// <param name="product">Product object</param>
    void Update(Product product);

    /// <summary>
    /// Deletes a product
    /// </summary>
    /// <param name="product">Product object</param>
    void Delete(Product product);

    /// <summary>
    /// Gets the total number of products
    /// </summary>
    Task<int> GetProductCountAsync();

    /// <summary>
    /// Gets the total inventory value
    /// </summary>
    Task<decimal> GetInventoryValueAsync();

    /// <summary>
    /// Gets the total number of units in stock
    /// </summary>
    Task<int> GetTotalUnitsInStockAsync();

    /// <summary>
    /// Gets the number of low-stock products
    /// </summary>
    Task<int> GetLowStockCountAsync(int threshold = 10);

    /// <summary>
    /// Gets the number of out-of-stock products
    /// </summary>
    Task<int> GetOutOfStockCountAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="threshold">Thresshold</param>
    /// <param name="take">Take</param>
    Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold = 10, int take = 5);

    /// <summary>
    /// Saves changes to the database
    /// </summary>
    Task SaveChangesAsync();
}