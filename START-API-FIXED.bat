@echo off
echo ========================================
echo Starting POS API Server
echo ========================================
echo.
echo Stopping any existing API processes...
taskkill /F /IM dotnet.exe /FI "WINDOWTITLE eq *POS.API*" 2>nul

echo.
echo Starting API on https://localhost:7173...
cd POS.API
dotnet run

pause

