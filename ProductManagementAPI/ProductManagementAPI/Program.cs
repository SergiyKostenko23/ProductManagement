using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Data;
using ProductManagementAPI.Interfaces;
using ProductManagementAPI.Middleware;
using ProductManagementAPI.Repositories;
using ProductManagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);

var dbDirectory = Path.Combine(AppContext.BaseDirectory, "Data");
Directory.CreateDirectory(dbDirectory);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();

var dbPath = Path.Combine(AppContext.BaseDirectory, "Data", "products.db");

builder.Services.AddDbContext<ProductDbContext>(options =>
{
    options.UseSqlite($"Data Source={dbPath}");
});

builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularClient", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        var db = scope.ServiceProvider.GetRequiredService<ProductDbContext>();

        db.Database.Migrate();

        logger.LogInformation("Database migrations applied successfully.");
    }
    catch (Exception ex)
    {
        logger.LogCritical(ex, "Failed to apply database migrations.");

        throw;
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseCors("AngularClient");

app.UseAuthorization();

app.MapControllers();

app.Run();
