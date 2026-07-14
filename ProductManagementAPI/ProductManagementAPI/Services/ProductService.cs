using ProductManagementAPI.Common.Enums;
using ProductManagementAPI.Data.Entities;
using ProductManagementAPI.DTOs.Product;
using ProductManagementAPI.Interfaces;

namespace ProductManagementAPI.Services;

/// <summary>
/// Product service
/// </summary>
public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly ILogger<ProductService> _logger;

    /// <summary>
    /// Product service constructor
    /// </summary>
    /// <param name="repository">Repository</param>
    /// <param name="logger">Logger</param>
    public ProductService(
        IProductRepository repository,
        ILogger<ProductService> logger)
    {
        _repository = repository;
        _logger     = logger;
    }

    /// <summary>
    /// Gets all products
    /// </summary>
    /// <returns>List of obtained products</returns>
    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    /// <summary>
    /// Gets a product by its ID
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>Obtained product</returns>
    public Task<Product?> GetByIdAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    /// <summary>
    /// Creates a new product
    /// </summary>
    /// <param name="dto">Create product data transfer object</param>
    /// <returns>Created product</returns>
    public async Task<Product> CreateAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            ProductNumber = await GenerateUniqueProductNumber(),
            Name          = dto.Name,
            Description   = dto.Description,
            Category      = dto.Category,
            Price         = dto.Price,
            Stock         = dto.Stock,
            CreatedAt     = DateTime.UtcNow,
            UpdatedAt     = DateTime.UtcNow
        };

        await _repository.AddAsync(product);
        await _repository.SaveChangesAsync();

        _logger.LogInformation(
            "Created product {ProductNumber} ({ProductName})",
            product.ProductNumber,
            product.Name);

        return product;
    }

    /// <summary>
    /// Generates a unique product number
    /// </summary>
    /// <returns>Product number</returns>
    private async Task<int> GenerateUniqueProductNumber()
    {
        var random = new Random();

        int productNumber;

        do
        {
            productNumber = random.Next(100000, 999999);
        }
        while (await _repository.GetByProductNumberAsync(productNumber) != null);

        return productNumber;
    }

    /// <summary>
    /// Updates an existing product
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="dto">Update product data transfer object</param>
    /// <returns>Updated product</returns>
    public async Task<Product?> UpdateAsync(int id, UpdateProductDto dto)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product == null)
        {
            return null;
        }

        product.Name        = dto.Name;
        product.Description = dto.Description;
        product.Category    = dto.Category;
        product.Price       = dto.Price;
        product.Stock       = dto.Stock;
        product.UpdatedAt   = DateTime.UtcNow;

        _repository.Update(product);

        await _repository.SaveChangesAsync();

        _logger.LogInformation(
            "Updated product {ProductNumber}",
            product.ProductNumber);

        return product;
    }

    /// <summary>
    /// Deletes a product by its ID
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>True if successful, false if not found</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product == null)
        {
            _logger.LogWarning(
                "Attempt to delete product with id {Id}, but it was not found.",
                id);

            return false;
        }

        _repository.Delete(product);

        await _repository.SaveChangesAsync();

        _logger.LogInformation(
            "Deleted product {ProductNumber}",
            product.ProductNumber);

        return true;
    }

    /// <summary>
    /// Gets a list of products that are within stock range
    /// </summary>
    /// <param name="min">Minimum stock</param>
    /// <param name="max">Maximum stock</param>
    /// <returns>List of products that were found</returns>
    /// <exception cref="ArgumentException"></exception>
    public Task<IEnumerable<Product>> GetByStockRangeAsync(int min, int max)
    {
        if (min < 0)
            throw new ArgumentException("Minimum stock cannot be negative.");

        if (max < min)
            throw new ArgumentException("Maximum stock must be greater than or equal to minimum stock.");

        return _repository.GetByStockRangeAsync(min, max);
    }

    /// <summary>
    /// Searches for products by name
    /// </summary>
    /// <param name="name">Product name</param>
    /// <returns>List of products that were found</returns>
    /// <exception cref="ArgumentException"></exception>
    public Task<IEnumerable<Product>> SearchByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Search term cannot be empty.");

        return _repository.SearchByNameAsync(name.Trim());
    }

    /// <summary>
    /// Searches for products by product number
    /// </summary>
    /// <param name="productNumber">Product number</param>
    /// <returns>List of products that were found</returns>
    /// <exception cref="ArgumentException"></exception>
    public Task<Product?> GetByProductNumberAsync(int productNumber)
    {
        if (productNumber < 100000 || productNumber > 999999)
            throw new ArgumentException("Product number must be within the following range: 100000 - 999999.");

        return _repository.GetByProductNumberAsync(productNumber);
    }

    /// <summary>
    /// Adds stock to a product
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="quantity">Quantity to be added</param>
    /// <returns>Action result</returns>
    public async Task<(StockUpdateResult, Product?)> AddStockAsync(int id, int quantity)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product == null)
        {
            _logger.LogWarning(
                "Attempt to add stock to unknown product {Id}",
                id);

            return (StockUpdateResult.ProductNotFound, null);
        }

        product.Stock     += quantity;
        product.UpdatedAt = DateTime.UtcNow;

        await _repository.SaveChangesAsync();

        _logger.LogInformation(
            "Added {Quantity} units to product {ProductNumber}. New stock: {Stock}",
            quantity,
            product.ProductNumber,
            product.Stock);

        return (StockUpdateResult.Success, product);
    }

    /// <summary>
    /// Decrements stock from a product
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="quantity">Quantity to be decremented</param>
    /// <returns>Action result</returns>
    public async Task<(StockUpdateResult, Product?)> DecrementStockAsync(int id, int quantity)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product == null)
        {
            _logger.LogWarning(
                "Attempt to decrement stock for unknown product {Id}",
                id);

            return (StockUpdateResult.ProductNotFound, null);
        }

        if (product.Stock < quantity)
        {
            _logger.LogWarning(
                "Insufficient stock for product {ProductNumber}. Requested: {Requested}, Available: {Available}",
                product.ProductNumber,
                quantity,
                product.Stock);

            return (StockUpdateResult.InsufficientStock, null);
        }

        product.Stock     -= quantity;
        product.UpdatedAt = DateTime.UtcNow;

        await _repository.SaveChangesAsync();

        _logger.LogInformation(
            "Removed {Quantity} units from product {ProductNumber}. Remaining stock: {Stock}",
            quantity,
            product.ProductNumber,
            product.Stock);

        return (StockUpdateResult.Success, product);
    }


}