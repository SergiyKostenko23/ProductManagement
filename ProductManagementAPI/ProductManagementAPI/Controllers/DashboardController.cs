using Microsoft.AspNetCore.Mvc;
using ProductManagementAPI.DTOs.Dashboard;
using ProductManagementAPI.Interfaces;

namespace ProductManagementAPI.Controllers
{
    /// <summary>
    /// Dashboard Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public sealed class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;

        /// <summary>
        /// Dashboard Controller Constructor
        /// </summary>
        /// <param name="service"></param>
        public DashboardController(
            IDashboardService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get Dashboard Data
        /// </summary>
        /// <returns>Dashboard data transfer object</returns>
        [HttpGet]
        [ProducesResponseType(typeof(DashboardDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<DashboardDto>> Get()
        {
            var dashboard =
                await _service.GetDashboardAsync();

            return Ok(dashboard);
        }

        /// <summary>
        /// Gets the list of products that are low in stock.
        /// </summary>
        /// <returns></returns>
        [HttpGet("low-stock")]
        [ProducesResponseType(typeof(IEnumerable<LowStockProductDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LowStockProductDto>>> GetLowStockProducts()
        {
            var products = await _service.GetLowStockProductsAsync();

            return Ok(products);
        }
    }
}
