# 🔧 Complete Fix Guide - Login API Integration

## Issues Fixed

✅ **Password Hashes** - Generated correct BCrypt hashes  
✅ **CORS Configuration** - Reordered middleware properly  
✅ **Angular @ Symbol** - Escaped with `&#64;` HTML entity  
✅ **API URL** - Updated to use HTTPS port 7173  
✅ **Connection String** - Fixed to use LocalDB  

## 📋 Quick Steps to Get Everything Working

### Step 1: Start the Backend API

Open a **new terminal** in `E:\Github\Pos\PosBackend` and run:

```bash
cd POS.API
dotnet run
```

**Expected output:**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:7173
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5216
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

⚠️ **If you see errors**, check:
- SQL Server LocalDB is installed
- Port 7173 is not in use

### Step 2: Verify API is Running

In another terminal:

```powershell
[System.Net.ServicePointManager]::ServerCertificateValidationCallback = {$true}
Invoke-RestMethod -Uri "https://localhost:7173/api/auth/health"
```

**Expected response:**
```json
{
  "status": "healthy",
  "timestamp": "2025-10-04T..."
}
```

### Step 3: Test Login Endpoint

```powershell
[System.Net.ServicePointManager]::ServerCertificateValidationCallback = {$true}
$body = @{ 
    email = "admin@cxp.com"
    password = "Admin123!" 
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://localhost:7173/api/auth/login" `
    -Method POST `
    -ContentType "application/json" `
    -Body $body
```

**Expected response:**
```json
{
  "success": true,
  "message": "Login successful",
  "user": {
    "id": 2,
    "name": "CXP Admin",
    "email": "admin@cxp.com",
    "role": "Admin"
  },
  "token": "..."
}
```

### Step 4: Start Angular App

In a separate terminal:

```bash
cd E:\Github\Pos\POS
ng serve
```

Navigate to `http://localhost:4200` and test login!

---

## 🔐 Test Credentials

**Admin Login:**
- Email: `admin@cxp.com`
- Password: `Admin123!`

**Shop Login:**
- Email: `downtown@mithai.com`
- Password: `shop123`

---

## 🐛 Troubleshooting

### API Won't Start

**Error: Port already in use**
```bash
# Kill existing processes
Get-Process | Where-Object {$_.ProcessName -eq "dotnet"} | Stop-Process -Force
```

**Error: Database connection failed**
- Ensure SQL Server LocalDB is installed
- Test connection: `sqllocaldb info mssqllocaldb`
- If not installed, install SQL Server Express LocalDB

### CORS Errors in Browser

Open browser DevTools (F12) and check the error. It should now show:
```
Access-Control-Allow-Origin: http://localhost:4200
```

If you still see CORS errors:
1. Make sure API is running on https://localhost:7173
2. Check `environment.ts` has correct URL: `https://localhost:7173/api`
3. Verify CORS middleware order in `Program.cs` (UseCors before UseHttpsRedirection)

### Angular Compilation Error

If you see: `NG5002: Incomplete block "mithai"...`

The @ symbols in HTML are now properly escaped as `&#64;`

Restart the Angular dev server:
```bash
# Stop with Ctrl+C, then restart
ng serve
```

### Login Still Fails with "Invalid email or password"

The database needs to be reseeded with correct hashes:

```bash
cd E:\Github\Pos\PosBackend
sqlcmd -S "(localdb)\mssqllocaldb" -d POSDB -Q "DELETE FROM Users; DBCC CHECKIDENT ('Users', RESEED, 0);"
```

Then restart the API - it will automatically reseed on startup.

---

## 📁 Files Modified

### Backend
- ✅ `POS.API/Program.cs` - Fixed CORS middleware order
- ✅ `POS.API/appsettings.json` - Fixed connection string
- ✅ `POS.Core/Data/DatabaseSeeder.cs` - Updated all password hashes

### Frontend
- ✅ `POS/src/environments/environment.ts` - Updated API URL to HTTPS
- ✅ `POS/src/app/login/login.component.html` - Escaped @ symbols
- ✅ `POS/src/app/services/auth.service.ts` - Added login() method with HttpClient
- ✅ `POS/src/app/login/login.component.ts` - Updated to use API authentication

---

## 🎯 What Changed

### CORS Configuration (Program.cs)
**Before:**
```csharp
app.UseHttpsRedirection();
app.UseCors("AllowAngular");
app.UseAuthorization();
```

**After:**
```csharp
app.UseCors("AllowAngular");  // MUST be first!
app.UseHttpsRedirection();
app.UseAuthorization();
```

### API URL (environment.ts)
**Before:**
```typescript
apiUrl: 'http://localhost:5216/api'
```

**After:**
```typescript
apiUrl: 'https://localhost:7173/api'
```

### Connection String (appsettings.json)
**Before:**
```json
"DefaultConnection": "USER_SECRET"
```

**After:**
```json
"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=POSDB;Trusted_Connection=true;MultipleActiveResultSets=true"
```

---

## ✅ Verification Checklist

- [ ] Backend API starts without errors
- [ ] Health endpoint responds: `https://localhost:7173/api/auth/health`
- [ ] Login endpoint works with PowerShell test
- [ ] Angular app compiles without errors
- [ ] Login form displays correctly (with @ symbols showing properly)
- [ ] Can login as admin: `admin@cxp.com` / `Admin123!`
- [ ] Can login as shop: `downtown@mithai.com` / `shop123`
- [ ] Redirects to correct page based on role (Admin → dashboard, Shop → orders)

---

## 🚀 Success!

Once all steps are complete, you should be able to:
1. Open Angular app at `http://localhost:4200`
2. Enter credentials
3. See "Authenticating..." loader
4. Get success message with user's name
5. Redirect to appropriate dashboard

**The login form is now fully integrated with your backend API!** 🎉

---

**Last Updated:** October 4, 2025  
**Status:** Ready to use ✅

