# 🔐 Test Credentials for POS Backend API

All users have been seeded in the database for testing purposes.

---

## 👤 Admin Users

### Admin #1
- **Email:** `admin@pos.com`
- **Password:** `Admin123!`
- **Role:** Admin
- **Status:** Active
- **Description:** Primary admin account

### Admin #2 (CXP)
- **Email:** `admin@cxp.com`
- **Password:** `Admin123!`
- **Role:** Admin
- **Status:** Active
- **Description:** Secondary admin account matching Angular frontend

---

## 🏪 Shop Users

### Downtown Mithai Shop
- **Email:** `downtown@mithai.com`
- **Password:** `shop123`
- **Role:** Shop
- **Status:** Active
- **Description:** Downtown branch shop account

### Mall Mithai Shop
- **Email:** `mall@mithai.com`
- **Password:** `shop123`
- **Role:** Shop
- **Status:** Active
- **Description:** Mall branch shop account

### Suburb Mithai Shop
- **Email:** `suburb@mithai.com`
- **Password:** `shop123`
- **Role:** Shop
- **Status:** Active
- **Description:** Suburb branch shop account

---

## 👥 Regular Users

### John Doe
- **Email:** `john.doe@example.com`
- **Password:** `user123`
- **Role:** User
- **Status:** Active
- **Description:** Standard user account

### Jane Smith
- **Email:** `jane.smith@example.com`
- **Password:** `user123`
- **Role:** User
- **Status:** Active
- **Description:** Standard user account

### Test User
- **Email:** `test@test.com`
- **Password:** `test123`
- **Role:** User
- **Status:** Active
- **Description:** Test user account

---

## 🚫 Inactive Users

### Inactive User
- **Email:** `inactive@example.com`
- **Password:** `inactive123`
- **Role:** User
- **Status:** ❌ **Inactive**
- **Description:** For testing inactive account handling (login should fail)

---

## 🧪 Testing Login

### PowerShell Example:
```powershell
# Test Admin Login
$body = @{
    email = "admin@cxp.com"
    password = "Admin123!"
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5216/api/auth/login" `
    -Method POST `
    -ContentType "application/json" `
    -Body $body
```

### cURL Example:
```bash
# Test Shop Login
curl -X POST http://localhost:5216/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"downtown@mithai.com","password":"shop123"}'
```

### JavaScript/TypeScript Example:
```typescript
// For Angular frontend
const credentials = {
  email: 'admin@cxp.com',
  password: 'Admin123!'
};

this.http.post('http://localhost:5216/api/auth/login', credentials)
  .subscribe(response => console.log(response));
```

---

## 📊 User Summary

| ID | Name | Email | Role | Status | Password |
|----|------|-------|------|--------|----------|
| 1 | Admin User | admin@pos.com | Admin | ✅ Active | Admin123! |
| 2 | CXP Admin | admin@cxp.com | Admin | ✅ Active | Admin123! |
| 3 | Downtown Mithai Shop | downtown@mithai.com | Shop | ✅ Active | shop123 |
| 4 | Mall Mithai Shop | mall@mithai.com | Shop | ✅ Active | shop123 |
| 5 | Suburb Mithai Shop | suburb@mithai.com | Shop | ✅ Active | shop123 |
| 6 | John Doe | john.doe@example.com | User | ✅ Active | user123 |
| 7 | Jane Smith | jane.smith@example.com | User | ✅ Active | user123 |
| 8 | Test User | test@test.com | User | ✅ Active | test123 |
| 9 | Inactive User | inactive@example.com | User | ❌ Inactive | inactive123 |

---

## 🎯 Expected Behaviors

### ✅ Successful Login
All active users (IDs 1-8) should:
- Return `"success": true`
- Include user data (id, name, email, role)
- Include authentication token
- Return HTTP 200

### ❌ Failed Login Scenarios

**1. Invalid Password:**
```json
{
  "success": false,
  "message": "Invalid email or password",
  "user": null,
  "token": null
}
```

**2. Non-existent Email:**
```json
{
  "success": false,
  "message": "Invalid email or password",
  "user": null,
  "token": null
}
```

**3. Inactive Account (ID 9):**
```json
{
  "success": false,
  "message": "Account is inactive. Please contact administrator.",
  "user": null,
  "token": null
}
```

---

## 🔒 Security Notes

1. **Password Hashing:** All passwords are hashed using BCrypt with work factor 11
2. **Never Log Passwords:** Passwords are never stored or logged in plain text
3. **Production:** Change all these credentials before deploying to production
4. **Token:** Currently using base64-encoded tokens; implement JWT for production

---

## 🔄 Reset Database

If you need to reset the database with fresh seed data:

```bash
# Option 1: Drop and recreate
dotnet ef database drop --project POS.Core --startup-project POS.API --force
dotnet ef database update --project POS.Core --startup-project POS.API

# Option 2: Reset specific data
# Run SQL script to truncate and reseed Users table
```

---

## 📝 Adding More Test Users

To add more test users, edit `POS.Core/Data/AppDbContext.cs`:

```csharp
new User
{
    Id = 10,
    Name = "Your Name",
    Email = "your.email@example.com",
    PasswordHash = "$2a$11$...", // Generate using BCrypt
    Role = "User",
    IsActive = true,
    CreatedAt = new DateTime(2025, 1, 5, 0, 0, 0, DateTimeKind.Utc)
}
```

Then create and apply a new migration:
```bash
dotnet ef migrations add AddNewUsers --project POS.Core --startup-project POS.API
dotnet ef database update --project POS.Core --startup-project POS.API
```

---

**Last Updated:** October 4, 2025  
**Total Users:** 9 (8 active, 1 inactive)  
**Roles:** Admin (2), Shop (3), User (4)

