using Microsoft.Extensions.Logging;
using Moq;
using ProductManagementAPI.Common.Enums;
using ProductManagementAPI.Data.Entities;
using ProductManagementAPI.DTOs.Product;
using ProductManagementAPI.Interfaces;
using ProductManagementAPI.Services;

namespace ProductManagementAPI.Tests.Services;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _repository;
    private readonly ProductService _service;

    public ProductServiceTests(ILogger<ProductService> logger)
    {
        _repository = new Mock<IProductRepository>();
        _service    = new ProductService(_repository.Object, logger);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateProduct()
    {
        var dto = new CreateProductDto
        {
            Name        = "Ray-Ban Aviator",
            Description = "Classic aviator sunglasses.",
            Category    = "Sunglasses",
            Price       = 199.99m,
            Stock       = 10
        };

        _repository
            .Setup(r => r.GetByProductNumberAsync(It.IsAny<int>()))
            .ReturnsAsync((Product?)null);

        var product = await _service.CreateAsync(dto);

        Assert.NotNull(product);
        Assert.Equal(dto.Name, product.Name);
        Assert.Equal(dto.Description, product.Description);
        Assert.Equal(dto.Category, product.Category);
        Assert.Equal(dto.Price, product.Price);
        Assert.Equal(dto.Stock, product.Stock);
        Assert.True(product.ProductNumber >= 100000 && product.ProductNumber <= 999999);
        Assert.NotEqual(default, product.CreatedAt);
        Assert.NotEqual(default, product.UpdatedAt);

        _repository.Verify(
            r => r.AddAsync(It.IsAny<Product>()),
            Times.Once);

        _repository.Verify(
            r => r.SaveChangesAsync(),
            Times.Once);
    }

    [Fact]
    public async Task AddStock_ShouldIncreaseStock()
    {
        var product = new Product
        {
            Id    = 1,
            Name  = "Ray-Ban Aviator",
            Stock = 10
        };

        _repository
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(product);

        var (result, updatedProduct) = await _service.AddStockAsync(1, 5);

        Assert.Equal(StockUpdateResult.Success, result);
        Assert.NotNull(updatedProduct);
        Assert.Equal(15, updatedProduct!.Stock);

        _repository.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task AddStock_ShouldReturnNull_WhenProductDoesNotExist()
    {
        _repository
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync((Product?)null);

        var (result, product) = await _service.AddStockAsync(1, 5);

        Assert.Equal(StockUpdateResult.ProductNotFound, result);
        Assert.Null(product);

        _repository.Verify(r => r.SaveChangesAsync(), Times.Never);
    }

    [Fact]
    public async Task DecrementStock_ShouldReturnInsufficientStock_WhenQuantityExceedsStock()
    {
        var product = new Product
        {
            Id    = 1,
            Name  = "Ray-Ban Aviator",
            Stock = 3
        };

        _repository
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(product);

        var (result, updatedProduct) = await _service.DecrementStockAsync(1, 10);

        Assert.Equal(StockUpdateResult.InsufficientStock, result);
        Assert.Null(updatedProduct);

        _repository.Verify(r => r.SaveChangesAsync(), Times.Never);
    }

    [Fact]
    public async Task DecrementStock_ShouldDecreaseStock_WhenSufficientStockExists()
    {
        var product = new Product
        {
            Id    = 1,
            Name  = "Ray-Ban Aviator",
            Stock = 10
        };

        _repository
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(product);

        var (result, updatedProduct) = await _service.DecrementStockAsync(1, 5);

        Assert.Equal(StockUpdateResult.Success, result);
        Assert.NotNull(updatedProduct);
        Assert.Equal(5, updatedProduct!.Stock);

        _repository.Verify(r => r.SaveChangesAsync(), Times.Never);
    }
}