# POS API Test Script
# This script tests the authentication endpoints

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "   POS API Test Script" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Configuration
$baseUrl = "http://localhost:5216"  # HTTP port from launchSettings.json
$httpsUrl = "https://localhost:7173"  # HTTPS port from launchSettings.json

# Function to test health endpoint
function Test-Health {
    Write-Host "Testing Health Endpoint..." -ForegroundColor Yellow
    try {
        $response = Invoke-RestMethod -Uri "$baseUrl/api/auth/health" -Method GET
        Write-Host "✓ Health Check Passed" -ForegroundColor Green
        Write-Host "Response: $($response | ConvertTo-Json)" -ForegroundColor Gray
        return $true
    }
    catch {
        Write-Host "✗ Health Check Failed: $_" -ForegroundColor Red
        return $false
    }
}

# Function to test login
function Test-Login {
    param (
        [string]$Email,
        [string]$Password
    )
    
    Write-Host "`nTesting Login for: $Email" -ForegroundColor Yellow
    
    $body = @{
        email = $Email
        password = $Password
    } | ConvertTo-Json
    
    try {
        $response = Invoke-RestMethod -Uri "$baseUrl/api/auth/login" `
            -Method POST `
            -ContentType "application/json" `
            -Body $body
        
        if ($response.success) {
            Write-Host "✓ Login Successful" -ForegroundColor Green
            Write-Host "User: $($response.user.name) ($($response.user.role))" -ForegroundColor Gray
            Write-Host "Token: $($response.token.Substring(0, [Math]::Min(50, $response.token.Length)))..." -ForegroundColor Gray
        }
        else {
            Write-Host "✗ Login Failed: $($response.message)" -ForegroundColor Red
        }
        
        return $response
    }
    catch {
        Write-Host "✗ Request Failed: $_" -ForegroundColor Red
        return $null
    }
}

# Run Tests
Write-Host "Starting API Tests..." -ForegroundColor Cyan
Write-Host ""

# Test 1: Health Check
$healthOk = Test-Health

if (-not $healthOk) {
    Write-Host "`n⚠ API may not be running. Please start it with 'cd POS.API && dotnet run'" -ForegroundColor Yellow
    exit
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "   Login Tests" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan

# Test 2: Admin Login
Write-Host "`n--- Admin Users ---" -ForegroundColor Cyan
Test-Login -Email "admin@cxp.com" -Password "Admin123!"

# Test 3: Shop Login
Write-Host "`n--- Shop Users ---" -ForegroundColor Cyan
Test-Login -Email "downtown@mithai.com" -Password "shop123"
Test-Login -Email "mall@mithai.com" -Password "shop123"

# Test 4: Regular User Login
Write-Host "`n--- Regular Users ---" -ForegroundColor Cyan
Test-Login -Email "test@test.com" -Password "test123"

# Test 5: Error Cases
Write-Host "`n--- Error Cases ---" -ForegroundColor Cyan
Test-Login -Email "admin@cxp.com" -Password "wrongpassword"
Test-Login -Email "notexist@pos.com" -Password "Admin123!"
Test-Login -Email "inactive@example.com" -Password "inactive123"

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "   Tests Complete!" -ForegroundColor Cyan
Write-Host "   See TEST-CREDENTIALS.md for all users" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan

