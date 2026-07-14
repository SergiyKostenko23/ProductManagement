using ProductManagementAPI.DTOs.Dashboard;
using ProductManagementAPI.Interfaces;
using ProductManagementAPI.Mappings;

namespace ProductManagementAPI.Services
{
    /// <summary>
    /// Dashboard service implementation
    /// </summary>
    public sealed class DashboardService : IDashboardService
    {
        private readonly IProductRepository _repository;

        /// <summary>
        /// Dashboard service constructor
        /// </summary>
        /// <param name="repository"></param>
        public DashboardService(IProductRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets the dashboard data asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task<DashboardDto> GetDashboardAsync()
        {
            return new DashboardDto
            {
                TotalProducts = await _repository.GetProductCountAsync(),

                TotalInventoryValue =
                    await _repository.GetInventoryValueAsync(),

                TotalUnitsInStock =
                    await _repository.GetTotalUnitsInStockAsync(),

                LowStockProducts =
                    await _repository.GetLowStockCountAsync(),

                OutOfStockProducts =
                    await _repository.GetOutOfStockCountAsync()
            };
        }

        /// <summary>
        /// Gets the low stock products asynchronously
        /// </summary>
        /// <returns>Low stock products data transfer objects</returns>
        public async Task<IEnumerable<LowStockProductDto>> GetLowStockProductsAsync()
        {
            var products = await _repository.GetLowStockProductsAsync();

            return products.Select(product => product.ToLowStockDto());
        }
    }
}
