# Quick script to generate BCrypt password hashes
# This helps create seed data with proper password hashes

Write-Host "Password Hash Generator for Seed Data" -ForegroundColor Cyan
Write-Host "======================================" -ForegroundColor Cyan
Write-Host ""

# Add the BCrypt NuGet package path
$bcryptDll = Get-ChildItem -Path "$env:USERPROFILE\.nuget\packages\bcrypt.net-next\4.0.3\lib" -Recurse -Filter "BCrypt.Net-Next.dll" | Select-Object -First 1

if ($null -eq $bcryptDll) {
    Write-Host "❌ BCrypt.Net-Next.dll not found. Please build the project first." -ForegroundColor Red
    exit
}

Add-Type -Path $bcryptDll.FullName

# Generate hashes
Write-Host "Generating BCrypt hashes..." -ForegroundColor Yellow
Write-Host ""

$passwords = @{
    "Admin123!" = "Admin User"
    "shop123" = "Shop Users"
    "user123" = "Regular User"
}

foreach ($pwd in $passwords.Keys) {
    $hash = [BCrypt.Net.BCrypt]::HashPassword($pwd, [BCrypt.Net.BCrypt]::GenerateSalt(11))
    Write-Host "$($passwords[$pwd]): $pwd" -ForegroundColor Green
    Write-Host "Hash: $hash" -ForegroundColor Gray
    Write-Host ""
}

Write-Host "✓ Done!" -ForegroundColor Green

