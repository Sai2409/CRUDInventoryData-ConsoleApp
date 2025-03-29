using ECommerceInventory.Models;

namespace ECommerceInventory.Repositories
{
    public interface IProductRepository
    {
        Task AddProductAsync(Product product);
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);

        Task UpdateStockQuantityAsync(int productId, int quantity);

        Task<int?> GetProductQuantityAsync(int productId);

        Task<bool> ProductExistsAsync(string name);
    }
}
