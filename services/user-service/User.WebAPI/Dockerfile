# =========================
# 1. Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Copy csproj files (optimize restore cache)
COPY User.WebAPI/User.WebAPI.csproj User.WebAPI/
COPY User.Application/User.Application.csproj User.Application/
COPY User.Domain/User.Domain.csproj User.Domain/
COPY User.Infrastructure/User.Infrastructure.csproj User.Infrastructure/
# Restore dependencies
RUN dotnet restore User.WebAPI/User.WebAPI.csproj
# Copy the rest of source code
COPY . .
# Build & publish
WORKDIR /src/User.WebAPI
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
ENTRYPOINT ["dotnet", "User.WebAPI.dll"]
