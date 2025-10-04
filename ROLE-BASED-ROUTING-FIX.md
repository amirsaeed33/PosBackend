# 🔧 Role-Based Routing Fix

## Problem
After login, users were not being redirected to the correct page based on their role (Admin → dashboard, Shop → orders).

## Root Cause
The user's **role** from the API response was not being stored in the `Shop` interface or persisted in localStorage, so it couldn't be used for routing decisions or future authentication checks.

## ✅ Solution Applied

### 1. Added `role` field to Shop interface
**File:** `POS/src/app/services/shop.service.ts`

```typescript
export interface Shop {
  id: number;
  name: string;
  email: string;
  phone: string;
  address: string;
  balance: number;
  role?: string;        // ✅ Added this field
  createdDate?: Date;
  lastUpdated?: Date;
}
```

### 2. Updated AuthService to store role from API response
**File:** `POS/src/app/services/auth.service.ts`

```typescript
const shopUser: ShopUser = {
  shop: {
    id: response.user.id,
    name: response.user.name,
    email: response.user.email,
    phone: '',
    address: '',
    balance: 0,
    role: response.user.role,  // ✅ Store the role from API
    createdDate: new Date(),
    lastUpdated: new Date()
  },
  loginTime: new Date(),
  isLoggedIn: true,
  token: response.token
};
```

### 3. Enhanced role checking methods
**File:** `POS/src/app/services/auth.service.ts`

```typescript
// ✅ Check by role first, fallback to ID
isAdmin(): boolean {
  const current = this.currentShopSubject.value;
  return current?.shop?.role?.toLowerCase() === 'admin' || 
         current?.shop?.id === 0;
}

isShop(): boolean {
  const current = this.currentShopSubject.value;
  return current?.shop?.role?.toLowerCase() === 'shop' || 
         (current?.shop?.id !== 0 && current?.shop?.id !== undefined);
}

// ✅ New method to get current user's role
getUserRole(): string | null {
  return this.currentShopSubject.value?.shop?.role || null;
}
```

### 4. Added console logging for debugging
**File:** `POS/src/app/login/login.component.ts`

```typescript
console.log('Login API Response:', response);
console.log('User Role:', response.user?.role);

const userRole = response.user?.role?.toLowerCase();
console.log('Redirecting user with role:', userRole);

if (userRole === 'admin') {
  console.log('Navigating to /dashboard');
  this.router.navigate(['/dashboard']);
} else {
  console.log('Navigating to /orders');
  this.router.navigate(['/orders']);
}
```

## 🧪 How to Test

### 1. Start Backend API
```bash
cd E:\Github\Pos\PosBackend\POS.API
dotnet run
```

### 2. Start Angular App
```bash
cd E:\Github\Pos\POS
ng serve
```

### 3. Test Admin Login
1. Go to `http://localhost:4200`
2. Login with:
   - Email: `admin@cxp.com`
   - Password: `Admin123!`
3. **Expected:** After 1 second, redirects to `/dashboard`
4. **Check Console:** Should see:
   ```
   Login API Response: {success: true, user: {...role: "Admin"}, token: "..."}
   User Role: Admin
   Redirecting user with role: admin
   Navigating to /dashboard
   ```

### 4. Test Shop Login
1. Logout or open incognito window
2. Login with:
   - Email: `downtown@mithai.com`
   - Password: `shop123`
3. **Expected:** After 1 second, redirects to `/orders`
4. **Check Console:** Should see:
   ```
   Login API Response: {success: true, user: {...role: "Shop"}, token: "..."}
   User Role: Shop
   Redirecting user with role: shop
   Navigating to /orders
   ```

## 📊 Role Mapping

| User Email | Password | Role | Expected Route |
|------------|----------|------|----------------|
| admin@cxp.com | Admin123! | Admin | /dashboard |
| admin@pos.com | Admin123! | Admin | /dashboard |
| downtown@mithai.com | shop123 | Shop | /orders |
| mall@mithai.com | shop123 | Shop | /orders |
| suburb@mithai.com | shop123 | Shop | /orders |
| john.doe@example.com | user123 | User | /orders |
| jane.smith@example.com | user123 | User | /orders |

## 🔍 Debugging Tips

### Check localStorage
After login, open browser DevTools → Application → Local Storage → `http://localhost:4200`

Look for `currentShop` key. It should contain:
```json
{
  "shop": {
    "id": 2,
    "name": "CXP Admin",
    "email": "admin@cxp.com",
    "role": "Admin",
    ...
  },
  "token": "...",
  "isLoggedIn": true
}
```

### Verify API Response
In browser DevTools → Network tab:
1. Filter by `XHR` or `Fetch`
2. Find `login` request
3. Click on it → Response tab
4. Should see:
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

## ✅ Benefits

1. **Role Persisted**: User role is now saved in localStorage
2. **Better Auth Checks**: Can use `authService.getUserRole()` anywhere
3. **Route Guards Ready**: Can create route guards using `isAdmin()` and `isShop()`
4. **Debug Logging**: Console logs help track authentication flow
5. **Backward Compatible**: Fallback checks ensure old code still works

## 🚀 Next Steps (Optional)

### Create Route Guards
To protect routes based on user role:

```typescript
// admin.guard.ts
export const AdminGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  
  if (authService.isAdmin()) {
    return true;
  }
  
  router.navigate(['/orders']);
  return false;
};

// In app.routes.ts
{
  path: 'dashboard',
  component: DashboardComponent,
  canActivate: [AdminGuard]
}
```

---

**Status:** ✅ Fixed and Tested  
**Date:** October 4, 2025  
**Impact:** Role-based routing now works correctly

