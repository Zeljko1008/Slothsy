using Microsoft.EntityFrameworkCore;
using Slothsy.Domain.Entities;
using System.Text.Json;

namespace Slothsy.Infrastructure.Data
{
    public static class DbInitializer
    {
        /// <summary>
        /// Initializes the database by applying migrations and seeding initial data.
        /// </summary>
        /// <param name="context">Database context.</param>
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

            // Seed ProductCategories
            if (!context.ProductCategories.Any())
            {
                var productCategoriesPath = Path.Combine(basePath, "productCategories.json");
                if (File.Exists(productCategoriesPath))
                {
                    var productCategoriesData = File.ReadAllText(productCategoriesPath);
                    var productCategories = JsonSerializer.Deserialize<List<ProductCategory>>(productCategoriesData, options)!;

                    context.ProductCategories.AddRange(productCategories);
                    await context.SaveChangesAsync();
                }
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

            //Seed SizeOptions
            try
            {
                if (!context.SizeOptions.Any())
            {
                var sizeOptionsPath = Path.Combine(basePath, "sizeOptions.json");
                var sizeOptionsData = File.ReadAllText(sizeOptionsPath);
                Console.WriteLine($"Size options JSON length: {sizeOptionsData.Length}");
                var sizeOptions = JsonSerializer.Deserialize<List<SizeOption>>(sizeOptionsData, options)!;
                context.SizeOptions.AddRange(sizeOptions);
                await context.SaveChangesAsync();
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding SizeOptions: {ex.Message}");
            }

            //Seed ColorOptions

            if (!context.ColorOptions.Any())
            {
                var colorOptionsPath = Path.Combine(basePath, "colorOptions.json");
                var colorOptionsData = File.ReadAllText(colorOptionsPath);
                var colorOptions = JsonSerializer.Deserialize<List<ColorOption>>(colorOptionsData, options)!;
                context.ColorOptions.AddRange(colorOptions);
                await context.SaveChangesAsync();
            }

            // Seed ProductVariants

            if (!context.ProductVariants.Any())
            {
                var productVariantsPath = Path.Combine(basePath, "productVariants.json");
                var productVariantsData = File.ReadAllText(productVariantsPath);
                var productVariants = JsonSerializer.Deserialize<List<ProductVariant>>(productVariantsData, options)!;
                context.ProductVariants.AddRange(productVariants);
                await context.SaveChangesAsync();
            }

            // Seed ProductVariantImages

            if (!context.ProductVariantImages.Any())
            {
                var productVariantImagesPath = Path.Combine(basePath, "productVariantImages.json");
                var productVariantImagesData = File.ReadAllText(productVariantImagesPath);
                var productVariantImages = JsonSerializer.Deserialize<List<ProductVariantImage>>(productVariantImagesData, options)!;
                context.ProductVariantImages.AddRange(productVariantImages);
                await context.SaveChangesAsync();
            }

            Console.WriteLine("[SEED DEBUG] Database seeding complete.");
        }
    }
}
