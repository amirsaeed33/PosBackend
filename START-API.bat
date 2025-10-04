@echo off
echo ========================================
echo   Starting POS Backend API
echo ========================================
echo.

cd POS.API
echo Starting API on http://localhost:5216
echo Swagger UI: http://localhost:5216/swagger
echo.
dotnet run

pause

