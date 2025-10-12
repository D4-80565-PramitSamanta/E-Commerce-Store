using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> GetProductsByFilter(string? brand, string? type, string? sort);

        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);

        void AddProduct(Product product);
        void UpdateProduct(Product product);

        void DeleteProduct(int id);

        bool ProductExists(int id);

        Task<bool> SaveChangesAsync();

        Task<IReadOnlyList<string>> GetBrandsAsync();
        Task<IReadOnlyList<string>> GetTypesAsync();
    }
}
