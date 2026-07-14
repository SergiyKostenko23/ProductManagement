using Microsoft.AspNetCore.Mvc;
using ProductManagementAPI.Common.Enums;
using ProductManagementAPI.Data.Entities;
using ProductManagementAPI.DTOs.Product;
using ProductManagementAPI.Interfaces;
using ProductManagementAPI.Mappings;

namespace ProductManagementAPI.Controllers;

/// <summary>
/// Products Controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    /// <summary>
    /// Products Controller Constructor
    /// </summary>
    /// <param name="service">Service</param>
    public ProductsController(IProductService service)
    {
        _service = service;
    }

    /// <summary>
    /// Gets all products
    /// </summary>
    /// <returns>List of obtained products</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
    {
        var products = await _service.GetAllAsync();

        return Ok(products.ToDtos());
    }

    /// <summary>
    /// Gets a product by its ID
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>Obtained product</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> GetById(int id)
    {
        var product = await _service.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product.ToDto());
    }

    /// <summary>
    /// Creates a new product
    /// </summary>
    /// <param name="dto">Create product data transfer object</param>
    /// <returns>Created product</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductDto>> Create(CreateProductDto dto)
    {
        var product = await _service.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = product.Id },
            product.ToDto());
    }

    /// <summary>
    /// Updates an existing product
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="dto">Update product data transfer object</param>
    /// <returns>Updated product</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductDto>> Update(
    int id,
    UpdateProductDto dto)
    {
        var product = await _service.UpdateAsync(id, dto);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product.ToDto());
    }

    /// <summary>
    /// Deletes a product by its ID
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>Action result</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Searches for products by name
    /// </summary>
    /// <param name="name">Product name</param>
    /// <returns>List of products that were found</returns>
    [HttpGet("search")]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> Search(string name)
    {
        var products = await _service.SearchByNameAsync(name);

        return Ok(products.ToDtos());
    }

    /// <summary>
    /// Searches for products by product number
    /// </summary>
    /// <param name="productNumber">Product number</param>
    /// <returns>List of products that were found</returns>
    [HttpGet("product-number/{productNumber:int}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> GetByProductNumber(int productNumber)
    {
        var product = await _service.GetByProductNumberAsync(productNumber);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product?.ToDto());
    }

    /// <summary>
    /// Gets a list of products that are within stock range
    /// </summary>
    /// <param name="min">Minimum stock</param>
    /// <param name="max">Maximum stock</param>
    /// <returns>List of products that were found</returns>
    [HttpGet("stock-level")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetByStockRange(
    int min,
    int max)
    {
        var products = await _service.GetByStockRangeAsync(min, max);

        return Ok(products.ToDtos());
    }

    /// <summary>
    /// Adds stock to a product
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="quantity">Quantity to be added</param>
    /// <returns>Action result</returns>
    [HttpPost("{id:int}/add-to-stock/{quantity:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddStock(
    int id,
    int quantity)
    {
        var (result, product) = await _service.AddStockAsync(id, quantity);

        return result switch 
        {
            StockUpdateResult.ProductNotFound => NotFound(),
            StockUpdateResult.Success         => Ok(product?.ToDto()),

            _ => BadRequest("An error occurred while adding stock.")
        };
    }

    /// <summary>
    /// Decrements stock from a product
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="quantity">Quantity to be decremented</param>
    /// <returns>Action result</returns>
    /// <exception cref="InvalidOperationException"></exception>
    [HttpPost("{id:int}/decrement-stock/{quantity:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DecrementStock(
    int id,
    int quantity)
    {
        var (result, product) = await _service.DecrementStockAsync(id, quantity);

        return result switch
        {
            StockUpdateResult.ProductNotFound   => NotFound(),
            StockUpdateResult.InsufficientStock => BadRequest("Not enough stock."),
            StockUpdateResult.Success           => Ok(product?.ToDto()),

            _ => throw new InvalidOperationException("Unexpected stock update result.")
        };
    }
}