@echo off
echo ========================================
echo   Starting POS Backend API
echo ========================================
echo.

cd POS.API
echo Starting API on http://localhost:5216
echo Swagger UI will open automatically in your browser...
echo.
echo If browser doesn't open, navigate to:
echo http://localhost:5216/swagger
echo.
dotnet run

pause

