namespace ProductManagementAPI.DTOs.Dashboard
{
    /// <summary>
    /// Dashboard data transfer object
    /// </summary>
    public sealed class DashboardDto
    {
        /// <summary>
        /// Total number of products
        /// </summary>
        public int TotalProducts { get; init; }

        /// <summary>
        /// Total inventory value (sum of price * stock for all products)
        /// </summary>
        public decimal TotalInventoryValue { get; init; }

        /// <summary>
        /// Total units in stock (sum of stock for all products)
        /// </summary>
        public int TotalUnitsInStock { get; init; }

        /// <summary>
        /// Low stock products (number of products with stock below a certain threshold)
        /// </summary>
        public int LowStockProducts { get; init; }

        /// <summary>
        /// Out of stock products (number of products with stock equal to zero)
        /// </summary>
        public int OutOfStockProducts { get; init; }
    }
}
