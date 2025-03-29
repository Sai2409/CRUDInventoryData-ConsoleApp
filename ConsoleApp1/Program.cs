using ECommerceInventory.Data;
using ECommerceInventory.Models;
using ECommerceInventory.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();


        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer("Server=localhost\\MSSQLSERVER01;Database=InventoryDB;Trusted_Connection=True;TrustServerCertificate=Yes"));

        services.AddScoped<IProductRepository, ProductRepository>();

        var serviceProvider = services.BuildServiceProvider();

        var repository = serviceProvider.GetService<IProductRepository>();

        while (true)
        {
            Console.WriteLine("\n========== E-Commerce Inventory System ==========");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. View Product");
            Console.WriteLine("3. View All Products");
            Console.WriteLine("4. Update Product");
            Console.WriteLine("5. Delete Product");
            Console.WriteLine("6. Update Stock Quantity");
            Console.WriteLine("7. View Product Quantity");
            Console.WriteLine("8. Exit");
            Console.Write("Enter your choice: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }

            try
            {
                switch (choice)
                {
                    case 1:
                        await AddProductAsync(repository);
                        break;
                    case 2:
                        await ViewProductAsync(repository);
                        break;
                    case 3:
                        await ViewAllProductsAsync(repository);
                        break;
                    case 4:
                        await UpdateProductAsync(repository);
                        break;
                    case 5:
                        await DeleteProductAsync(repository);
                        break;
                    case 6:
                        await UpdateStockQuantityAsync(repository);
                        break;
                    case 7:
                        await GetProductQuantityAsync(repository);
                        break;
                    case 8:
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    static async Task AddProductAsync(IProductRepository repository)
    {
        Console.Write("Enter Product Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Price: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            Console.WriteLine("Invalid price input. Operation cancelled.");
            return;
        }
        try
        {
            await repository.AddProductAsync(new Product { Name = name, Price = price });
            Console.WriteLine("Product added successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static async Task ViewProductAsync(IProductRepository repository)
    {
        Console.Write("Enter Product ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid input. Operation cancelled.");
            return;
        }

        var product = await repository.GetProductByIdAsync(id);
        if (product != null)
        {
            Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}");
        }
        else
        {
            Console.WriteLine("Product not found.");
        }
    }

    static async Task ViewAllProductsAsync(IProductRepository repository)
    {
        var products = await repository.GetAllProductsAsync();
        if (products.Any())
        {
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}");
            }
        }
        else
        {
            Console.WriteLine("No products available.");
        }
    }

    static async Task UpdateProductAsync(IProductRepository repository)
    {
        Console.Write("Enter Product ID to update: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid input. Operation cancelled.");
            return;
        }

        var product = await repository.GetProductByIdAsync(id);
        if (product == null)
        {
            Console.WriteLine("Product not found.");
            return;
        }

        Console.Write("Enter new Name (or press Enter to keep current): ");
        string newName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newName))
        {
            product.Name = newName;
        }

        Console.Write("Enter new Price (or press Enter to keep current): ");
        string priceInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(priceInput) && decimal.TryParse(priceInput, out decimal newPrice))
        {
            product.Price = newPrice;
        }

        await repository.UpdateProductAsync(product);
        Console.WriteLine("Product updated successfully.");
    }

    static async Task DeleteProductAsync(IProductRepository repository)
    {
        Console.Write("Enter Product ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid input. Operation cancelled.");
            return;
        }

        await repository.DeleteProductAsync(id);
        Console.WriteLine("Product deleted successfully.");
    }

    static async Task UpdateStockQuantityAsync(IProductRepository repository)
    {
        Console.Write("Enter Product ID: ");
        if (!int.TryParse(Console.ReadLine(), out int productId))
        {
            Console.WriteLine("Invalid input. Operation cancelled.");
            return;
        }

        Console.Write("Enter new Quantity: ");
        if (!int.TryParse(Console.ReadLine(), out int newQuantity))
        {
            Console.WriteLine("Invalid input. Operation cancelled.");
            return;
        }

        await repository.UpdateStockQuantityAsync(productId, newQuantity);
        Console.WriteLine("Stock quantity updated successfully.");
    }

    static async Task GetProductQuantityAsync(IProductRepository repository)
    {
        Console.Write("Enter Product ID: ");
        if (!int.TryParse(Console.ReadLine(), out int productId))
        {
            Console.WriteLine("Invalid input. Operation cancelled.");
            return;
        }

        int? quantity = await repository.GetProductQuantityAsync(productId);
        Console.WriteLine($"Stock Quantity for Product ID {productId}: {quantity}");
    }
}
