# POS Backend API

## 🚀 Project Overview

A modern .NET 9.0 Web API for Point of Sale (POS) system with clean architecture:

- **POS.API**: Web API layer with controllers
- **POS.Core**: Data layer with entities, DbContext, and migrations  
- **POS.Services**: Business logic layer with services and interfaces

## 📋 Prerequisites

- .NET 9.0 SDK
- SQL Server or LocalDB
- Visual Studio 2022 / VS Code / Rider

## 🏗️ Architecture

```
PosBackend/
├── POS.API/              # Web API Controllers & Configuration
├── POS.Core/             # Domain Entities & Database Context
│   ├── Data/            # DbContext
│   ├── Entities/        # Domain Models
│   └── Migrations/      # EF Core Migrations
└── POS.Services/         # Business Logic
    ├── DTOs/            # Data Transfer Objects
    ├── Interfaces/      # Service Interfaces
    └── Services/        # Service Implementations
```

## 🔧 Setup Instructions

### 1. Restore Dependencies

```bash
dotnet restore
```

### 2. Update Connection String

Edit `POS.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=POSDB;Trusted_Connection=true;"
  }
}
```

### 3. Create Database

```bash
dotnet ef database update --project POS.Core --startup-project POS.API
```

### 4. Run the API

```bash
cd POS.API
dotnet run
```

The API will start at:
- **HTTPS**: https://localhost:7XXX
- **HTTP**: http://localhost:5XXX
- **Swagger UI**: https://localhost:7XXX/swagger

## 📡 API Endpoints

### Authentication

#### **POST** `/api/auth/login`

Login with email and password.

**Request Body:**
```json
{
  "email": "admin@pos.com",
  "password": "Admin123!"
}
```

**Success Response (200):**
```json
{
  "success": true,
  "message": "Login successful",
  "user": {
    "id": 1,
    "name": "Admin User",
    "email": "admin@pos.com",
    "role": "Admin"
  },
  "token": "base64encodedtoken"
}
```

**Error Response (200):**
```json
{
  "success": false,
  "message": "Invalid email or password",
  "user": null,
  "token": null
}
```

#### **GET** `/api/auth/health`

Health check endpoint.

**Response (200):**
```json
{
  "status": "healthy",
  "timestamp": "2025-10-04T16:00:00Z"
}
```

## 🔐 Test Credentials

The database is seeded with 9 test users:

**Quick Access:**
- Admin: `admin@cxp.com` / `Admin123!`
- Shop: `downtown@mithai.com` / `shop123`
- User: `test@test.com` / `test123`

📄 **See [TEST-CREDENTIALS.md](TEST-CREDENTIALS.md) for complete list with all roles and passwords**

## 🧪 Testing with cURL

### Test Login
```bash
curl -X POST https://localhost:7XXX/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"admin@pos.com","password":"Admin123!"}'
```

### Test Health
```bash
curl https://localhost:7XXX/api/auth/health
```

## 🧪 Testing with PowerShell

### Test Login
```powershell
$body = @{
    email = "admin@pos.com"
    password = "Admin123!"
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://localhost:7XXX/api/auth/login" `
    -Method POST `
    -ContentType "application/json" `
    -Body $body
```

## 📦 NuGet Packages Used

**POS.Core:**
- Microsoft.EntityFrameworkCore (9.0.9)
- Microsoft.EntityFrameworkCore.SqlServer (9.0.9)
- Microsoft.EntityFrameworkCore.Tools (9.0.9)
- BCrypt.Net-Next (4.0.3)

**POS.Services:**
- BCrypt.Net-Next (4.0.3)

**POS.API:**
- Microsoft.EntityFrameworkCore.Design (9.0.9)
- Swashbuckle.AspNetCore (9.0.6)

## 🔄 Common Commands

### Create New Migration
```bash
dotnet ef migrations add MigrationName --project POS.Core --startup-project POS.API
```

### Apply Migrations
```bash
dotnet ef database update --project POS.Core --startup-project POS.API
```

### Remove Last Migration
```bash
dotnet ef migrations remove --project POS.Core --startup-project POS.API
```

### Build Solution
```bash
dotnet build
```

### Run Tests (when added)
```bash
dotnet test
```

## 🌐 CORS Configuration

The API is configured to accept requests from:
- `http://localhost:4200` (Angular dev server)
- `https://*.ngrok-free.dev` (ngrok tunnels)
- `https://*.ngrok-free.app` (ngrok tunnels)
- `https://*.ngrok.io` (ngrok tunnels)

## 📝 Database Schema

### Users Table

| Column | Type | Constraints |
|--------|------|-------------|
| Id | int | PRIMARY KEY, IDENTITY |
| Name | nvarchar(100) | NOT NULL |
| Email | nvarchar(100) | NOT NULL, UNIQUE |
| PasswordHash | nvarchar(255) | NOT NULL |
| Role | nvarchar(50) | DEFAULT 'User' |
| IsActive | bit | DEFAULT 1 |
| CreatedAt | datetime2 | DEFAULT GETUTCDATE() |
| UpdatedAt | datetime2 | NULL |

## 🛠️ Development Tips

1. **Use AsNoTracking()**: Applied for read-only queries (per user requirement)
2. **Password Hashing**: BCrypt with work factor 11
3. **Swagger UI**: Available in development mode
4. **Logging**: Configured for Entity Framework queries

## 📄 License

This project is for internal use.

## 👥 Support

For issues or questions, contact the development team.

