# Online Store - ASP.NET Core E-Commerce Application



A full-featured e-commerce web application built with ASP.NET Core 6 MVC, Entity Framework Core, and modern web technologies. This project demonstrates professional development practices including the repository pattern, dependency injection, and clean architecture principles.

## 📋 Table of Contents

- [About the Project](#about-the-project)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Architecture](#architecture)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Database Setup](#database-setup)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [Screenshots](#screenshots)
- [Roadmap](#roadmap)
- [Contributing](#contributing)
- [License](#license)
- [Acknowledgments](#acknowledgments)
- [Contact](#contact)

## 🎯 About the Project

This Online Store is a comprehensive e-commerce solution developed following the **Complete ASP.NET Core Course** from Udemy. The application showcases modern web development practices and includes all essential features required for a production-ready online shopping platform.

The project emphasizes:
- Clean, maintainable code architecture
- Test-driven development principles
- Secure authentication and authorization
- Responsive design for all devices
- Scalable database design

## ✨ Features

### Customer Features
- **Product Browsing**
  - Browse products by categories and subcategories
  - Product filtering and sorting
  - Detailed product views with images and descriptions

- **Shopping Cart**
  - Add/remove items from cart
  - Update quantities
  - Real-time price calculations
  - Cart persistence across sessions

- **User Account Management**
  - User registration and login
  - Profile management
  - Order history viewing
  - Password reset functionality

- **Checkout Process**
  - Multi-step checkout flow
  - Shipping address management
  - Order review and confirmation

### Admin Features
- **Product Management**
  - Create, read, update, and delete (CRUD) operations for products
  - Category and subcategory management
  - Product image upload and management
  - Inventory tracking

- **Order Management**
  - View and manage customer orders
  - Update order status
  - Order fulfillment tracking

- **User Management**
  - View registered users
  - Role-based access control
  - User activity monitoring

- **Dashboard**
  - Sales analytics and reporting
  - Revenue tracking
  - Popular products insights

## 🛠️ Technologies Used

### Backend
- **ASP.NET Core MVC** - Web framework
- **Entity Framework Core** - ORM for database operations
- **ASP.NET Core Identity** - Authentication and authorization
- **SQL Server** - Primary database
- **Repository Pattern** - Data access abstraction
- **Unit of Work Pattern** - Transaction management

### Frontend
- **Razor Pages** - Server-side rendering
- **Bootstrap 4** - Responsive UI framework
- **jQuery** - DOM manipulation and AJAX
- **JavaScript/ES6** - Client-side interactivity
- **Font Awesome** - Icons
- **Toastr** - Notification alerts

### Development Tools
- **Visual Studio** - Primary IDE
- **SQL Server Management Studio** - Database management
- **Git** - Version control
- **NuGet** - Package management

## 🏗️ Architecture

The project follows a layered architecture pattern:

```
Online-Store/
│
├── OnlineStore.Web/              # Presentation Layer
│   ├── Controllers/              # MVC Controllers
│   ├── Views/                    # Razor Views
│   ├── ViewModels/              # View Models
│   ├── wwwroot/                 # Static files (CSS, JS, Images)
│   └── Areas/                   # Admin and Customer areas
│
├── OnlineStore.Data/            # Data Access Layer
│   ├── Repository/              # Repository pattern implementation
│   ├── Migrations/              # EF Core migrations
│   └── ApplicationDbContext.cs  # Database context
│
├── OnlineStore.Models/          # Domain Models
│   ├── Entities/                # Database entities
│   └── ViewModels/              # Data transfer objects
│
└── OnlineStore.Utility/         # Shared Utilities
    ├── SD.cs                    # Static details and constants
    └── EmailSender.cs           # Email service
```

### Design Patterns Implemented
- **Repository Pattern**: Abstracts data access logic
- **Unit of Work Pattern**: Manages transactions across repositories
- **Dependency Injection**: Loose coupling between components
- **MVC Pattern**: Separation of concerns
- **ViewModel Pattern**: Data encapsulation for views

## 🚀 Getting Started




### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/maelsabbagh/Online-Store.git
   cd Online-Store
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Update the connection string**
   
   Open `appsettings.json` in the `OnlineStore.Web` project and update the connection string:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=OnlineStoreDB;Trusted_Connection=True;MultipleActiveResultSets=true"
     }
   }
   ```

### Database Setup

1. **Apply Entity Framework migrations**
   ```bash
   cd OnlineStore.Web
   dotnet ef database update
   ```

2. **Seed initial data (if available)**
   
   The database will be seeded with initial categories and an admin user on first run.

3. **Default Admin Credentials**
   ```
   Email: admin@onlinestore.com
   Password: Admin@123
   ```

## 💻 Usage

### Running the Application

1. **Using Visual Studio**
   - Open `OnlineStore.sln`
   - Set `OnlineStore.Web` as the startup project
   - Press `F5` or click the "Run" button

2. **Using Command Line**
   ```bash
   cd OnlineStore.Web
   dotnet run
   ```

3. **Access the application**
   - Navigate to `https://localhost:5001` or `http://localhost:5000`
   - Admin panel: `https://localhost:5001/Admin`

### Running Tests

```bash
dotnet test
```

## 📁 Project Structure

```
├── Controllers/
│   ├── HomeController.cs         # Homepage and product listing
│   ├── CartController.cs         # Shopping cart operations
│   ├── OrderController.cs        # Order processing
│   └── AccountController.cs      # User authentication
│
├── Areas/
│   ├── Admin/
│   │   ├── Controllers/          # Admin-specific controllers
│   │   └── Views/                # Admin views
│   └── Customer/
│       ├── Controllers/          # Customer-specific controllers
│       └── Views/                # Customer views
│
├── Models/
│   ├── Product.cs                # Product entity
│   ├── Category.cs               # Category entity
│   ├── ApplicationUser.cs        # Extended Identity user
│   ├── ShoppingCart.cs           # Cart entity
│   └── Order.cs                  # Order entity
│
├── ViewModels/
│   ├── ProductViewModel.cs       # Product display model
│   ├── CartViewModel.cs          # Shopping cart model
│   └── OrderViewModel.cs         # Order summary model
│
└── Data/
    ├── Repository/
    │   ├── IRepository.cs        # Generic repository interface
    │   ├── Repository.cs         # Generic repository implementation
    │   └── UnitOfWork.cs         # Unit of Work implementation
    └── ApplicationDbContext.cs   # EF Core DbContext
```

## 📸 Screenshots

### Customer Interface
- **Homepage**: Product carousel and featured categories
- **Product Listing**: Grid/list view with filters
- **Product Details**: High-quality images, descriptions, and reviews
- **Shopping Cart**: Itemized cart with quantity controls
- **Checkout**: Multi-step checkout process

### Admin Panel
- **Dashboard**: Sales overview and analytics
- **Product Management**: CRUD operations with image upload
- **Order Management**: Order status tracking and fulfillment
- **User Management**: User roles and permissions

## 🗺️ Roadmap

- [x] Basic e-commerce functionality
- [x] User authentication and authorization
- [x] Shopping cart and checkout
- [x] Admin panel


## 🤝 Contributing

Contributions are what make the open-source community an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request


## 📞 Contact

**Project Maintainer**: Mohamed Elsabbagh

- GitHub: [@maelsabbagh](https://github.com/maelsabbagh)
- Project Link: [https://github.com/maelsabbagh/Online-Store](https://github.com/maelsabbagh/Online-Store)

---

