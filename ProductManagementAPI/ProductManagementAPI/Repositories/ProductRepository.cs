using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Data;
using ProductManagementAPI.Data.Entities;
using ProductManagementAPI.Interfaces;

namespace ProductManagementAPI.Repositories;

/// <summary>
/// Product repository
/// </summary>
public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;

    /// <summary>
    /// Product repository constructor
    /// </summary>
    /// <param name="context"></param>
    public ProductRepository(ProductDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets all products
    /// </summary>
    /// <returns>List of obtained products</returns>
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products
            .OrderBy(p => p.Id)
            .ToListAsync();
    }

    /// <summary>
    /// Gets a product by its ID
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>Obtained product</returns>
    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
        .FirstOrDefaultAsync(p => p.Id == id);
    }

    /// <summary>
    /// Gets a product by its product number
    /// </summary>
    /// <param name="productNumber">Product number</param>
    /// <returns>Obtained product</returns>
    public async Task<Product?> GetByProductNumberAsync(int productNumber)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.ProductNumber == productNumber);
    }

    /// <summary>
    /// Searches for products by name
    /// </summary>
    /// <param name="name">Product name</param>
    /// <returns>List of products that were found</returns>
    public async Task<IEnumerable<Product>> SearchByNameAsync(string name)
    {
        return await _context.Products
            .Where(p => p.Name.ToLower().Contains(name.ToLower()))
            .OrderBy(p => p.Id)
            .ToListAsync();
    }

    /// <summary>
    /// Gets a list of products that are within stock range
    /// </summary>
    /// <param name="min">Minimum stock</param>
    /// <param name="max">Maximum stock</param>
    /// <returns>List of products that were found</returns>
    public async Task<IEnumerable<Product>> GetByStockRangeAsync(int min, int max)
    {
        return await _context.Products
            .Where(p => p.Stock >= min && p.Stock <= max)
            .OrderBy(p => p.Id)
            .ToListAsync();
    }

    /// <summary>
    /// Creates a new product
    /// </summary>
    /// <param name="product">Product to be created</param>
    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    /// <summary>
    /// Updates an existing product
    /// </summary>
    /// <param name="product">Product object</param>
    public void Update(Product product)
    {
        _context.Products.Update(product);
    }

    /// <summary>
    /// Deletes a product
    /// </summary>
    /// <param name="product">Product object</param>
    public void Delete(Product product)
    {
        _context.Products.Remove(product);
    }

    /// <summary>
    /// Gets the total count of products in the database
    /// </summary>
    /// <returns></returns>
    public async Task<int> GetProductCountAsync()
    {
        return await _context.Products.CountAsync();
    }

    /// <summary>
    /// Gets the total inventory value of all products in the database
    /// </summary>
    /// <returns>Inventory value</returns>
    public async Task<decimal> GetInventoryValueAsync()
    {
        return await _context.Products
            .SumAsync(p => p.Price * p.Stock);
    }

    /// <summary>
    /// Gets the total units in stock of all products in the database
    /// </summary>
    /// <returns>Amount of units</returns>
    public async Task<int> GetTotalUnitsInStockAsync()
    {
        return await _context.Products
            .SumAsync(p => p.Stock);
    }

    /// <summary>
    /// Gets the number of products that are low in stock, based on a threshold value
    /// </summary>
    /// <param name="threshold">Threshold amount</param>
    /// <returns>Count amount</returns>
    public async Task<int> GetLowStockCountAsync(int threshold = 10)
    {
        return await _context.Products
            .CountAsync(p => p.Stock > 0 && p.Stock < threshold);
    }

    /// <summary>
    /// Gets the number of products that are out of stock (stock equal to zero)
    /// </summary>
    /// <returns>Amount of products</returns>
    public async Task<int> GetOutOfStockCountAsync()
    {
        return await _context.Products
            .CountAsync(p => p.Stock == 0);
    }

    /// <summary>
    /// Gets the products with low stock.
    /// </summary>
    /// <param name="threshold">Maximum stock level considered low.</param>
    /// <param name="take">Maximum number of products to return.</param>
    /// <returns>Low stock products.</returns>
    public async Task<IEnumerable<Product>> GetLowStockProductsAsync(
        int threshold = 10,
        int take = 5)
    {
        return await _context.Products
            .Where(product => product.Stock > 0 &&
                              product.Stock < threshold)
            .OrderBy(product => product.Stock)
            .ThenBy(product => product.Name)
            .Take(take)
            .ToListAsync();
    }

    /// <summary>
    /// Saves changes to the database
    /// </summary>
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}