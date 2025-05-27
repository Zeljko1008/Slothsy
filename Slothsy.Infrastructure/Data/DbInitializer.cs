using Microsoft.EntityFrameworkCore;
using Slothsy.Domain.Entities;
using System.Text.Json;

namespace Slothsy.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(AppDbContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                await context.Database.MigrateAsync();
            }

            var basePath = Path.Combine(AppContext.BaseDirectory, "Data", "SeedData");
            Console.WriteLine($"[SEED DEBUG] Looking for seed data in: {basePath}");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            // Seed Categories
            if (!context.Categories.Any())
            {
                var categoriesPath = Path.Combine(basePath, "categories.json");
                var categoriesData = File.ReadAllText(categoriesPath);
                var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData, options)!;

                context.Categories.AddRange(categories);
                await context.SaveChangesAsync();
            }

            // Seed Products
            if (!context.Products.Any())
            {
                var productsPath = Path.Combine(basePath, "products.json");
                var productsData = File.ReadAllText(productsPath);
                var products = JsonSerializer.Deserialize<List<Product>>(productsData, options)!;

                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }

            // Seed DeliveryMethods
            if (!context.DeliveryMethods.Any())
            {
                var deliveryPath = Path.Combine(basePath, "deliveryMethods.json");
                var deliveryData = File.ReadAllText(deliveryPath);
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData, options)!;

                context.DeliveryMethods.AddRange(deliveryMethods);
                await context.SaveChangesAsync();
            }

            Console.WriteLine("[SEED DEBUG] Database seeding complete.");
        }
    }
}
