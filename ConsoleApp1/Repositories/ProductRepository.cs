using ECommerceInventory.Data;
using ECommerceInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceInventory.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ProductExistsAsync(string name)
        {
            return await _context.Products.AnyAsync(p => p.Name == name);
        }

        public async Task AddProductAsync(Product product)
        {
            if (await ProductExistsAsync(product.Name))
            {
                throw new Exception("Product already exists.");
            }


            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();


            var stockAvailability = new ProductStockAvailability
            {
                ProductId = product.Id,
                QuantityAvailable = null
            };

            await _context.ProductStockAvailabilities.AddAsync(stockAvailability);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            var stock = await _context.ProductStockAvailabilities.FirstOrDefaultAsync(s => s.ProductId == id);
            if (stock != null)
            {
                _context.ProductStockAvailabilities.Remove(stock);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStockQuantityAsync(int productId, int quantity)
        {
            var stock = await _context.ProductStockAvailabilities.FirstOrDefaultAsync(s => s.ProductId == productId);
            if (stock != null)
            {
                stock.QuantityAvailable = quantity;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Product stock not found.");
            }
        }

        public async Task<int?> GetProductQuantityAsync(int productId)
        {
            var stock = await _context.ProductStockAvailabilities.FirstOrDefaultAsync(s => s.ProductId == productId);
            if (stock != null)
            {
                return stock.QuantityAvailable;
            }
            else
            {
                throw new Exception("Product stock not found.");
            }
        }
    }
}
