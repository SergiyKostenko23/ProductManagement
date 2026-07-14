using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductManagementAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    Category = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "Name", "Price", "ProductNumber", "Stock", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Lenses", new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Premium single vision prescription lenses.", "ZEISS Single Vision Lenses", 149.99m, 100001, 45, new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, "Frames", new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Lightweight titanium frame with a modern design.", "Titanium Eyeglass Frame", 189.99m, 100002, 12, new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, "Frames", new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Classic acetate frame available in multiple colors.", "Acetate Eyeglass Frame", 129.99m, 100003, 30, new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, "Glasses", new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Reduce eye strain from prolonged screen exposure.", "Blue Light Blocking Glasses", 89.99m, 100004, 55, new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, "Sunglasses", new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc), "UV400 polarized sunglasses for outdoor activities.", "Polarized Sunglasses", 159.99m, 100005, 22, new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, "Lenses", new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Lenses that automatically adapt to changing light conditions.", "Photochromic Lenses", 279.99m, 100006, 16, new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, "Lens Treatments", new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Premium anti-glare coating for prescription lenses.", "Anti-Reflective Lens Coating", 59.99m, 100007, 75, new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, "Contact Lenses", new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Disposable daily contact lenses for maximum comfort.", "Daily Contact Lenses (30 Pack)", 34.99m, 100008, 120, new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, "Contact Lens Care", new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Multi-purpose disinfecting and cleaning solution.", "Contact Lens Solution", 14.99m, 100009, 80, new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, "Accessories", new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Reusable microfiber cloth for cleaning lenses.", "Microfiber Cleaning Cloth", 5.99m, 100010, 200, new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 11, "Accessories", new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Protective hard shell carrying case.", "Hard Shell Glasses Case", 24.99m, 100011, 65, new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 12, "Accessories", new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Compact repair kit with screws, nose pads and screwdriver.", "Eyeglass Repair Kit", 19.99m, 100012, 40, new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 13, "Frames", new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Durable and flexible frame designed for children.", "Children's Eyeglass Frame", 99.99m, 100013, 28, new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 14, "Accessories", new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Alcohol-free cleaning spray for glasses and sunglasses.", "Lens Cleaning Spray", 8.99m, 100014, 150, new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductNumber",
                table: "Products",
                column: "ProductNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
