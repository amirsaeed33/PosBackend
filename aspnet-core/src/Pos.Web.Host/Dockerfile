FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["src/Pos.Web.Host/Pos.Web.Host.csproj", "src/Pos.Web.Host/"]
COPY ["src/Pos.Web.Core/Pos.Web.Core.csproj", "src/Pos.Web.Core/"]
COPY ["src/Pos.Application/Pos.Application.csproj", "src/Pos.Application/"]
COPY ["src/Pos.Core/Pos.Core.csproj", "src/Pos.Core/"]
COPY ["src/Pos.EntityFrameworkCore/Pos.EntityFrameworkCore.csproj", "src/Pos.EntityFrameworkCore/"]
WORKDIR "/src/src/Pos.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/Pos.Web.Host", "src/Pos.Web.Host"]
COPY ["src/Pos.Web.Core", "src/Pos.Web.Core"]
COPY ["src/Pos.Application", "src/Pos.Application"]
COPY ["src/Pos.Core", "src/Pos.Core"]
COPY ["src/Pos.EntityFrameworkCore", "src/Pos.EntityFrameworkCore"]
WORKDIR "/src/src/Pos.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "Pos.Web.Host.dll"]
