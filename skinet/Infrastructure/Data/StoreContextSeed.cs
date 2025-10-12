using Core.Entities;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if (context.Products.Any() == false)
            {
                var prductsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(prductsData);

                if (products == null) { return; }

                context.Products.AddRange(products);

                await context.SaveChangesAsync();
            }
        }
    }
}
