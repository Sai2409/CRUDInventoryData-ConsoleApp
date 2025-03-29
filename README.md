# CRUD Inventory Management System
*A Console Application using C#, Entity Framework, and SQL Server*

## 📌 Overview
This project is a **CRUD-based inventory management system** built using **C# and Entity Framework (EF)**. It enables users to perform **Create, Read, Update, and Delete (CRUD) operations** on an inventory database. The application follows best practices like **Repository Pattern, Unit of Work Pattern, and Dependency Injection** for better maintainability.

## 🚀 Features
✅ **Product Management**: Add, update, delete, and retrieve product details.  
✅ **Stock Availability Tracking**: Manage stock levels for each product.  
✅ **Database Integration**: Uses **SQL Server/PostgreSQL** with **Entity Framework Core** for data persistence.  
✅ **Design Patterns**: Implements **Repository Pattern & Unit of Work Pattern** for clean architecture.  
✅ **Exception Handling**: Manages database exceptions (e.g., unique constraints, referential integrity).  
✅ **Dependency Injection**: Ensures better code maintainability and testability.  

## 🛠️ Technologies Used
- **Language:** C#  
- **Framework:** .NET Core  
- **ORM:** Entity Framework Core  
- **Database:** SQL Server / PostgreSQL  
- **Design Patterns:** Repository Pattern, Unit of Work Pattern, Dependency Injection  

## 📂 Project Structure
```
📦 CRUDInventoryData-ConsoleApp  
 ┣ 📂 Models  
 ┃ ┣ 📜 Product.cs  
 ┃ ┣ 📜 ProductStockAvailability.cs  
 ┣ 📂 Data  
 ┃ ┣ 📜 ApplicationDbContext.cs  
 ┃ ┣ 📜 DbInitializer.cs  
 ┣ 📂 Repositories  
 ┃ ┣ 📜 IProductRepository.cs  
 ┃ ┣ 📜 ProductRepository.cs  
 ┃ ┣ 📜 UnitOfWork.cs  
 ┣ 📂 Services  
 ┃ ┣ 📜 ProductService.cs  
 ┣ 📜 Program.cs  
 ┣ 📜 appsettings.json  
 ┣ 📜 README.md  
```

## 🛠️ Setup & Installation

### 1️⃣ Clone the Repository
```bash
git clone https://github.com/Sai2409/CRUDInventoryData-ConsoleApp.git
cd CRUDInventoryData-ConsoleApp
```

### 2️⃣ Configure Database Connection
Modify the `appsettings.json` file to match your SQL Server/PostgreSQL configuration:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=InventoryDB;Trusted_Connection=True;"
}
```

### 3️⃣ Apply Migrations & Update Database
Run the following commands to set up the database schema:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4️⃣ Run the Application
```bash
dotnet run
```

## 📝 CRUD Operations Example

### 📌 Add a Product
```csharp
Product product = new Product { Name = "Laptop", Price = 1200 };
_productRepository.Add(product);
_unitOfWork.Commit();
```

### 📌 Retrieve All Products
```csharp
var products = _productRepository.GetAll();
foreach (var product in products)
{
    Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}");
}
```

## 📌 Future Enhancements
🚀 Add a **REST API layer** for web access  
🚀 Implement **unit tests** for business logic  
🚀 Extend functionality to support **product categories**  

## 🤝 Contributing
Feel free to **fork this repository**, create a new branch, and submit a pull request. Contributions are always welcome! 🚀  

## 📩 Contact
👤 **Sai Durga Teja Donga**  
📧 [saidurgatejadonga@gmail.com](mailto:saidurgatejadonga@gmail.com)  
🔗 [LinkedIn](https://www.linkedin.com/in/sai-durga-teja-donga)  
