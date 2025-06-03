using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Slothsy.Application.Interfaces;
using Slothsy.Application.Mappings;
using Slothsy.Application.Services;
using Slothsy.Domain.Interfaces.RepositoryContracts;
using Slothsy.Infrastructure.Data;
using Slothsy.Infrastructure.Persistance.Repositories;



var builder = WebApplication.CreateBuilder(args);


// ------------------------------------------------------------
// Register application services and repositories
// ------------------------------------------------------------
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductReadService, ProductReadService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryReadService, CategoryReadService>();




// ------------------------------------------------------------
// Configure database context with SQL Server connection string
// ------------------------------------------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ------------------------------------------------------------
// Register AutoMapper profiles from the specified assembly
// ------------------------------------------------------------
builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);

// ------------------------------------------------------------
// Add controllers (API endpoints)
// ------------------------------------------------------------
builder.Services.AddControllers();

// ------------------------------------------------------------
// Add Swagger/OpenAPI support for API documentation
// ------------------------------------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ------------------------------------------------------------
// Configure CORS policy to allow requests from any origin
// ------------------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
    builder.WithOrigins("http://localhost:4200", "https://localhost:4200")
           .AllowAnyHeader()
           .AllowAnyMethod()
           .AllowCredentials();
    });
});

var app = builder.Build();

// ------------------------------------------------------------
// Use centralized error handling endpoint (only in production)
// ------------------------------------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ------------------------------------------------------------
// Initialize and seed the database on app startup
// ------------------------------------------------------------
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        await DbInitializer.InitializeAsync(context);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error during DB initialization: {ex.Message}");
    }
}


app.UseStaticFiles();
// ------------------------------------------------------------
// Enforce HTTPS redirection
// ------------------------------------------------------------
app.UseHttpsRedirection();

// ------------------------------------------------------------
// Use routing middleware to match incoming requests to endpoints
// ------------------------------------------------------------
app.UseRouting();

// ------------------------------------------------------------
// Use CORS middleware to apply the defined policy
// ------------------------------------------------------------
app.UseCors();

// ------------------------------------------------------------
// Use authentication middleware to validate user credentials
// ------------------------------------------------------------
app.UseAuthorization();

// ------------------------------------------------------------
// Map controller routes for incoming requests
// ------------------------------------------------------------

app.MapControllers();

// ------------------------------------------------------------
// Run the application
// ------------------------------------------------------------
app.Run();
