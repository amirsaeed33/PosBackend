# 🚀 Quick Start Guide

## Start the API in 3 Steps

### Option 1: Using Batch File (Easiest)
```bash
# Double-click or run:
START-API.bat
```

### Option 2: Using Command Line
```bash
cd POS.API
dotnet run
```

### Option 3: Using Visual Studio
1. Open `POS.sln`
2. Set `POS.API` as startup project
3. Press F5

---

## 🌐 Access Points

Once started, the API will be available at:

- **HTTP**: http://localhost:5216
- **HTTPS**: https://localhost:7173
- **Swagger UI**: http://localhost:5216/swagger
- **Health Check**: http://localhost:5216/api/auth/health

---

## 🧪 Test the API

### Method 1: Run Test Script
```powershell
.\test-api.ps1
```

### Method 2: Use Swagger UI
1. Navigate to http://localhost:5216/swagger
2. Expand `/api/Auth/login` endpoint
3. Click "Try it out"
4. Enter credentials:
   ```json
   {
     "email": "admin@pos.com",
     "password": "Admin123!"
   }
   ```
5. Click "Execute"

### Method 3: Use PowerShell
```powershell
$body = @{
    email = "admin@pos.com"
    password = "Admin123!"
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5216/api/auth/login" `
    -Method POST `
    -ContentType "application/json" `
    -Body $body
```

### Method 4: Use cURL
```bash
curl -X POST http://localhost:5216/api/auth/login \
  -H "Content-Type: application/json" \
  -d "{\"email\":\"admin@pos.com\",\"password\":\"Admin123!\"}"
```

---

## 📝 Expected Response

### Success Response:
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
  "token": "MTo..."
}
```

### Error Response:
```json
{
  "success": false,
  "message": "Invalid email or password",
  "user": null,
  "token": null
}
```

---

## 🔐 Test Credentials

**Admin Users:**
- Email: `admin@cxp.com` / Password: `Admin123!` (Role: Admin)
- Email: `admin@pos.com` / Password: `Admin123!` (Role: Admin)

**Shop Users:**
- Email: `downtown@mithai.com` / Password: `shop123` (Role: Shop)
- Email: `mall@mithai.com` / Password: `shop123` (Role: Shop)
- Email: `suburb@mithai.com` / Password: `shop123` (Role: Shop)

**Regular Users:**
- Email: `test@test.com` / Password: `test123` (Role: User)
- Email: `john.doe@example.com` / Password: `user123` (Role: User)

📄 **See [TEST-CREDENTIALS.md](TEST-CREDENTIALS.md) for complete list of all 9 test users**

---

## 🐛 Troubleshooting

### Port Already in Use?
Edit `POS.API/Properties/launchSettings.json` and change the port numbers:
```json
"applicationUrl": "http://localhost:YOUR_PORT"
```

### Database Not Found?
Run migration:
```bash
dotnet ef database update --project POS.Core --startup-project POS.API
```

### Connection String Issues?
Edit `POS.API/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_CONNECTION_STRING"
  }
}
```

### Can't Login?
The default admin user is seeded automatically. If you still can't login, run:
```bash
# Update the admin password using SQL
sqlcmd -S (localdb)\mssqllocaldb -d POSDB -i UpdateAdminPassword.sql
```

---

## 📊 Project Structure

```
PosBackend/
├── POS.API/              # 🌐 Web API
│   ├── Controllers/      #   - AuthController
│   └── Program.cs        #   - Startup configuration
├── POS.Core/             # 💾 Data Layer
│   ├── Data/            #   - AppDbContext
│   ├── Entities/        #   - User entity
│   └── Migrations/      #   - EF migrations
└── POS.Services/         # 💼 Business Logic
    ├── DTOs/            #   - LoginRequest, LoginResponse
    ├── Interfaces/      #   - IAuthService
    └── Services/        #   - AuthService
```

---

## 🎯 Next Steps

1. ✅ API is running
2. ✅ Test login endpoint
3. 🔄 Connect Angular frontend
4. 🔄 Add more controllers (Products, Orders, etc.)
5. 🔄 Implement JWT authentication
6. 🔄 Add unit tests

---

## 📚 More Information

See [README.md](README.md) for complete documentation.

