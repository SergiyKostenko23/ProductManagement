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
            new Product
            {
                Id = 1,
                ProductNumber = 100001,
                Name = "ZEISS Single Vision Lenses",
                Description = "Premium single vision prescription lenses.",
                Category = "Lenses",
                Price = 149.99m,
                Stock = 45,
                CreatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 2,
                ProductNumber = 100002,
                Name = "Titanium Eyeglass Frame",
                Description = "Lightweight titanium frame with a modern design.",
                Category = "Frames",
                Price = 189.99m,
                Stock = 12,
                CreatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 3,
                ProductNumber = 100003,
                Name = "Acetate Eyeglass Frame",
                Description = "Classic acetate frame available in multiple colors.",
                Category = "Frames",
                Price = 129.99m,
                Stock = 30,
                CreatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 4,
                ProductNumber = 100004,
                Name = "Blue Light Blocking Glasses",
                Description = "Reduce eye strain from prolonged screen exposure.",
                Category = "Glasses",
                Price = 89.99m,
                Stock = 55,
                CreatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 5,
                ProductNumber = 100005,
                Name = "Polarized Sunglasses",
                Description = "UV400 polarized sunglasses for outdoor activities.",
                Category = "Sunglasses",
                Price = 159.99m,
                Stock = 22,
                CreatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 6,
                ProductNumber = 100006,
                Name = "Photochromic Lenses",
                Description = "Lenses that automatically adapt to changing light conditions.",
                Category = "Lenses",
                Price = 279.99m,
                Stock = 16,
                CreatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 7,
                ProductNumber = 100007,
                Name = "Anti-Reflective Lens Coating",
                Description = "Premium anti-glare coating for prescription lenses.",
                Category = "Lens Treatments",
                Price = 59.99m,
                Stock = 75,
                CreatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 8,
                ProductNumber = 100008,
                Name = "Daily Contact Lenses (30 Pack)",
                Description = "Disposable daily contact lenses for maximum comfort.",
                Category = "Contact Lenses",
                Price = 34.99m,
                Stock = 120,
                CreatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 9,
                ProductNumber = 100009,
                Name = "Contact Lens Solution",
                Description = "Multi-purpose disinfecting and cleaning solution.",
                Category = "Contact Lens Care",
                Price = 14.99m,
                Stock = 80,
                CreatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 10,
                ProductNumber = 100010,
                Name = "Microfiber Cleaning Cloth",
                Description = "Reusable microfiber cloth for cleaning lenses.",
                Category = "Accessories",
                Price = 5.99m,
                Stock = 200,
                CreatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 11,
                ProductNumber = 100011,
                Name = "Hard Shell Glasses Case",
                Description = "Protective hard shell carrying case.",
                Category = "Accessories",
                Price = 24.99m,
                Stock = 65,
                CreatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 12,
                ProductNumber = 100012,
                Name = "Eyeglass Repair Kit",
                Description = "Compact repair kit with screws, nose pads and screwdriver.",
                Category = "Accessories",
                Price = 19.99m,
                Stock = 40,
                CreatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 13,
                ProductNumber = 100013,
                Name = "Children's Eyeglass Frame",
                Description = "Durable and flexible frame designed for children.",
                Category = "Frames",
                Price = 99.99m,
                Stock = 28,
                CreatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 14,
                ProductNumber = 100014,
                Name = "Lens Cleaning Spray",
                Description = "Alcohol-free cleaning spray for glasses and sunglasses.",
                Category = "Accessories",
                Price = 8.99m,
                Stock = 150,
                CreatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc)
            }


        }.ToArray();
    }
}