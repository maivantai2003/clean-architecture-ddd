# =========================
# 1. Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Copy csproj files (optimize restore cache)
COPY Auth.WebAPI/Auth.WebAPI.csproj Auth.WebAPI/
COPY Auth.Application/Auth.Application.csproj Auth.Application/
COPY Auth.Domain/Auth.Domain.csproj Auth.Domain/
COPY Auth.Infrastructure/Auth.Infrastructure.csproj Auth.Infrastructure/
# Restore dependencies
RUN dotnet restore Auth.WebAPI/Auth.WebAPI.csproj
# Copy the rest of source code
COPY . .
# Build & publish
WORKDIR /src/Auth.WebAPI
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false
# =========================
# 2. Runtime stage
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Expose port
EXPOSE 8080

# Copy published output
COPY --from=build /app/publish .
# Set environment
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "Auth.WebAPI.dll"]
