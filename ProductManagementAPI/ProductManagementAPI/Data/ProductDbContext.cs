using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Data.Entities;

namespace ProductManagementAPI.Data;

/// <summary>
/// Product database context
/// </summary>
public class ProductDbContext : DbContext
{
    /// <summary>
    /// Product database context constructor
    /// </summary>
    /// <param name="options">Options</param>
    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Products DbSet
    /// </summary>
    public DbSet<Product> Products => Set<Product>();

    /// <summary>
    /// OnModelCreating method
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .HasIndex(p => p.ProductNumber)
            .IsUnique();

        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Product>().HasData(
            PopulateData());
    }

    /// <summary>
    /// Populates the database with initial data
    /// </summary>
    /// <returns>Array of products</returns>
    private Product[] PopulateData()
    {
        return new List<Product>()
        {
                new() { Id = 1, ProductNumber = 100001, Name = "ZEISS Aviator Classic", Category = "Sunglasses", Price = 149.99m, Stock = 2 },
                new() { Id = 2, ProductNumber = 100002, Name = "Wayfarer Black", Category = "Sunglasses", Price = 169.99m, Stock = 0 },
                new() { Id = 3, ProductNumber = 100003, Name = "Sport Vision", Category = "Sunglasses", Price = 119.99m, Stock = 5 },
                new() { Id = 4, ProductNumber = 100004, Name = "Kids Flex", Category = "Glasses", Price = 79.99m, Stock = 18 },
                new() { Id = 5, ProductNumber = 100005, Name = "Executive Frame", Category = "Glasses", Price = 249.99m, Stock = 12 },
                new() { Id = 6, ProductNumber = 100006, Name = "Blue Light Basic", Category = "Glasses", Price = 59.99m, Stock = 48 },
                new() { Id = 7, ProductNumber = 100007, Name = "Blue Light Premium", Category = "Glasses", Price = 109.99m, Stock = 32 },
                new() { Id = 8, ProductNumber = 100008, Name = "Daily Contact Lens", Category = "Contact Lenses", Price = 39.99m, Stock = 75 },
                new() { Id = 9, ProductNumber = 100009, Name = "Monthly Contact Lens", Category = "Contact Lenses", Price = 69.99m, Stock = 54 },
                new() { Id = 10, ProductNumber = 100010, Name = "Astigmatism Lens", Category = "Contact Lenses", Price = 84.99m, Stock = 4 },
                new() { Id = 11, ProductNumber = 100011, Name = "Lens Cleaning Spray", Category = "Accessories", Price = 12.99m, Stock = 96 },
                new() { Id = 12, ProductNumber = 100012, Name = "Microfiber Cloth", Category = "Accessories", Price = 5.99m, Stock = 140 },
                new() { Id = 13, ProductNumber = 100014, Name = "Premium Glasses Case", Category = "Accessories", Price = 34.99m, Stock = 8 },
                new() { Id = 14, ProductNumber = 100013, Name = "Hard Glasses Case", Category = "Accessories", Price = 19.99m, Stock = 63 },
                new() { Id = 15, ProductNumber = 100015, Name = "Reading Glasses +1.5", Category = "Reading Glasses", Price = 29.99m, Stock = 27 },
                new() { Id = 16, ProductNumber = 100016, Name = "Reading Glasses +2.0", Category = "Reading Glasses", Price = 29.99m, Stock = 16 },
                new() { Id = 17, ProductNumber = 100017, Name = "Reading Glasses +2.5", Category = "Reading Glasses", Price = 29.99m, Stock = 6 },
                new() { Id = 18, ProductNumber = 100018, Name = "Titanium Frame", Category = "Glasses", Price = 319.99m, Stock = 9 },
                new() { Id = 19, ProductNumber = 100019, Name = "Carbon Fiber Frame", Category = "Glasses", Price = 359.99m, Stock = 3 },
                new() { Id = 20, ProductNumber = 100020, Name = "Luxury Sunglasses", Category = "Sunglasses", Price = 499.99m, Stock = 11 },
                new() { Id = 21, ProductNumber = 100021, Name = "Polarized Explorer", Category = "Sunglasses", Price = 189.99m, Stock = 21 },
                new() { Id = 22, ProductNumber = 100022, Name = "Retro Round Frame", Category = "Glasses", Price = 129.99m, Stock = 14 },
                new() { Id = 23, ProductNumber = 100023, Name = "Kids Reading Glasses", Category = "Reading Glasses", Price = 24.99m, Stock = 37 },
                new() { Id = 24, ProductNumber = 100024, Name = "Lens Cleaning Kit", Category = "Accessories", Price = 18.99m, Stock = 82 },
                new() { Id = 25, ProductNumber = 100025, Name = "Travel Glasses Case", Category = "Accessories", Price = 27.99m, Stock = 26 },
                new() { Id = 26, ProductNumber = 100026, Name = "Designer Frame Alpha", Category = "Glasses", Price = 289.99m, Stock = 5 },
                new() { Id = 27, ProductNumber = 100027, Name = "Designer Frame Beta", Category = "Glasses", Price = 279.99m, Stock = 2 },
                new() { Id = 28, ProductNumber = 100028, Name = "Premium Contact Lens", Category = "Contact Lenses", Price = 99.99m, Stock = 42 },
                new() { Id = 29, ProductNumber = 100029, Name = "Hydrating Lens Solution", Category = "Accessories", Price = 14.99m, Stock = 67 },
                new() { Id = 30, ProductNumber = 100030, Name = "Sports Goggles", Category = "Sports", Price = 159.99m, Stock = 9 },
                new() { Id = 31, ProductNumber = 100031, Name = "Cycling Sunglasses", Category = "Sports", Price = 199.99m, Stock = 17 },
                new() { Id = 32, ProductNumber = 100032, Name = "Swimming Goggles", Category = "Sports", Price = 34.99m, Stock = 45 },
                new() { Id = 33, ProductNumber = 100033, Name = "Office Blue Light", Category = "Glasses", Price = 89.99m, Stock = 58 },
                new() { Id = 34, ProductNumber = 100034, Name = "Luxury Titanium Frame", Category = "Glasses", Price = 429.99m, Stock = 4 },
                new() { Id = 35, ProductNumber = 100035, Name = "Minimalist Frame", Category = "Glasses", Price = 119.99m, Stock = 33 },
                new() { Id = 36, ProductNumber = 100036, Name = "Photochromic Glasses", Category = "Glasses", Price = 219.99m, Stock = 15 },
                new() { Id = 37, ProductNumber = 100037, Name = "Clip-On Sunglasses", Category = "Accessories", Price = 44.99m, Stock = 28 },
                new() { Id = 38, ProductNumber = 100038, Name = "Leather Glasses Case", Category = "Accessories", Price = 39.99m, Stock = 19 },
                new() { Id = 39, ProductNumber = 100039, Name = "Compact Repair Kit", Category = "Accessories", Price = 11.99m, Stock = 91 },
                new() { Id = 40, ProductNumber = 100040, Name = "Premium Microfiber Cloth", Category = "Accessories", Price = 9.99m, Stock = 125 },
                new() { Id = 41, ProductNumber = 100041, Name = "Reading Glasses +3.0", Category = "Reading Glasses", Price = 34.99m, Stock = 8 },
                new() { Id = 42, ProductNumber = 100042, Name = "Reading Glasses +3.5", Category = "Reading Glasses", Price = 34.99m, Stock = 3 },
                new() { Id = 43, ProductNumber = 100043, Name = "Business Collection Frame", Category = "Glasses", Price = 259.99m, Stock = 22 },
                new() { Id = 44, ProductNumber = 100044, Name = "Classic Metal Frame", Category = "Glasses", Price = 139.99m, Stock = 39 },
                new() { Id = 45, ProductNumber = 100045, Name = "Ultra Thin Lens Pack", Category = "Contact Lenses", Price = 109.99m, Stock = 18 },
                new() { Id = 46, ProductNumber = 100046, Name = "Night Driving Glasses", Category = "Glasses", Price = 149.99m, Stock = 7 },
                new() { Id = 47, ProductNumber = 100047, Name = "Vintage Aviator", Category = "Sunglasses", Price = 174.99m, Stock = 11 },
                new() { Id = 48, ProductNumber = 100048, Name = "Fashion Cat Eye", Category = "Sunglasses", Price = 159.99m, Stock = 24 },
                new() { Id = 49, ProductNumber = 100049, Name = "Protective Safety Glasses", Category = "Safety", Price = 49.99m, Stock = 71 },
                new() { Id = 50, ProductNumber = 100050, Name = "Industrial Safety Goggles", Category = "Safety", Price = 64.99m, Stock = 13 }
        }.ToArray();
    }
}