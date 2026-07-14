using ProductManagementAPI.DTOs.Dashboard;

namespace ProductManagementAPI.Interfaces
{
    /// <summary>
    /// Dashboard service interface
    /// </summary>
    public interface IDashboardService
    {
        /// <summary>
        /// Gets the dashboard data asynchronously.
        /// </summary>
        /// <returns>Dashboard data transfer object</returns>
        Task<DashboardDto> GetDashboardAsync();

        /// <summary>
        /// Gets the low stock products asynchronously.
        /// </summary>
        /// <returns>Low stock products data transfer objects</returns>
        Task<IEnumerable<LowStockProductDto>> GetLowStockProductsAsync();
    }
}
