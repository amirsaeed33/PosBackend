@echo off
echo ========================================
echo Resetting POS Database
echo ========================================
echo.

echo Dropping existing database...
dotnet ef database drop --project POS.Core --startup-project POS.API --force

echo.
echo Applying migrations...
dotnet ef database update --project POS.Core --startup-project POS.API

echo.
echo ========================================
echo Database reset complete!
echo The database will be seeded automatically on next API startup.
echo ========================================
echo.
pause

