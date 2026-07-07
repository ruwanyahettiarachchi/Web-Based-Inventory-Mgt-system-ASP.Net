# Inventory Management System (ASP.NET Core MVC)

A web-based inventory management system built with **ASP.NET Core MVC**, **Entity Framework Core**, and **MySQL**. It supports managing product categories, tracking stock levels, recording sales, and generating exportable sales reports.

## ✨ Features

- **Category management** — create, view, edit, and delete product categories
- **Product management** — create, view, edit, and delete products, each linked to a category, with quantity and price tracking
- **Sales tracking** — record sales transactions (product, quantity sold, sale date)
- **Sales reports** — view sales totals grouped by date
- **Excel export** — download the sales report as a `.xlsx` file (powered by EPPlus)
- Server-rendered, form-based CRUD UI using Razor views and Bootstrap

## 🛠 Tech Stack

- **.NET 8** / ASP.NET Core MVC
- **Entity Framework Core 8** with **Pomelo.EntityFrameworkCore.MySql** (MySQL provider)
- **EPPlus** for Excel report generation
- Razor views (`.cshtml`)

## 📂 Project Structure

```
InventoryManagementSystem/
├── Controllers/
│   ├── CategoriesController.cs  # CRUD for categories
│   ├── ProductsController.cs    # CRUD for products
│   ├── SalesController.cs       # CRUD for sales records
│   ├── ReportController.cs      # Sales report view + Excel export
│   └── HomeController.cs        # Landing page
├── Models/
│   ├── Category.cs               # Category entity
│   ├── Product.cs                 # Product entity (linked to Category)
│   ├── Sale.cs                    # Sale entity (linked to Product)
│   ├── SalesReportViewModel.cs    # View model for the sales report
│   └── InventoryDbContext.cs      # EF Core DbContext
├── Views/
│   ├── Categories/  # Index, Create, Edit, Details, Delete
│   ├── Products/    # Index, Create, Edit, Details, Delete
│   ├── Sales/       # Index, Create, Edit, Details, Delete
│   ├── Report/      # Sales report index (with Excel export link)
│   └── Shared/      # Layout, error page
└── Program.cs        # App startup, MySQL DbContext registration
```

## 🗄 Data Model

| Entity | Key Fields | Relationships |
|---|---|---|
| `Category` | `CategoryId`, `Name` | Has many `Product`s |
| `Product` | `ProductId`, `Name`, `Quantity`, `Price`, `CategoryId` | Belongs to a `Category` |
| `Sale` | `SaleId`, `ProductId`, `QuantitySold`, `SaleDate` | Belongs to a `Product` |

The sales report groups `Sale` records by date and computes `TotalSales = Σ(QuantitySold × Product.Price)` per day.

## 🚦 Application Routes

| Controller | Typical routes |
|---|---|
| Categories | `/Categories`, `/Categories/Create`, `/Categories/Edit/{id}`, `/Categories/Details/{id}`, `/Categories/Delete/{id}` |
| Products | `/Products`, `/Products/Create`, `/Products/Edit/{id}`, `/Products/Details/{id}`, `/Products/Delete/{id}` |
| Sales | `/Sales`, `/Sales/Create`, `/Sales/Edit/{id}`, `/Sales/Details/{id}`, `/Sales/Delete/{id}` |
| Reports | `/Report` (view), `/Report/ExportToExcel` (download `.xlsx`) |

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- MySQL Server (local install or Docker container)

### Setup

1. Clone the repository
   ```bash
   git clone https://github.com/ruwanyahettiarachchi/Web-Based-Inventory-Mgt-system-ASP.Net.git
   cd Web-Based-Inventory-Mgt-system-ASP.Net/InventoryManagementSystem
   ```
2. Update the connection string in `appsettings.json` to match your local MySQL setup:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "server=localhost;database=InventoryDB;user=root;password=YOUR_PASSWORD;"
   }
   ```
   > ⚠️ Avoid committing real credentials — for anything beyond local development, move this into an environment variable or `dotnet user-secrets`.
3. Apply migrations / create the database (if migrations are present) or let EF Core create it from the `InventoryDbContext`.
4. Run the app:
   ```bash
   dotnet restore
   dotnet run
   ```
5. Open the URL shown in the console and navigate to Products, Categories, Sales, or Reports.

## 🧠 Key Concepts to Learn From This Project

- Modeling **one-to-many relationships** in EF Core (`Category → Products`, `Product → Sales`)
- Writing **LINQ aggregation queries** (`GroupBy`, `Sum`) to build a report view model
- Generating downloadable files from a controller action (`File()` result + in-memory `MemoryStream`)
- Using a **third-party library (EPPlus)** to build Excel workbooks programmatically
- Connecting ASP.NET Core to **MySQL** via a community EF Core provider (Pomelo) instead of SQL Server
- Standard **ASP.NET Core MVC scaffolding conventions** (Index/Create/Edit/Details/Delete per controller)

## 📄 License

This project is available for personal and educational use. Feel free to fork and adapt it for your own learning purposes.
