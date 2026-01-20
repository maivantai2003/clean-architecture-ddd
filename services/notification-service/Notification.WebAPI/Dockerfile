# =========================
# 1. Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Copy csproj files (optimize restore cache)
COPY Notification.WebAPI/Notification.WebAPI.csproj Notification.WebAPI/
COPY Notification.Application/Notification.Application.csproj Notification.Application/
COPY Notification.Domain/Notification.Domain.csproj Notification.Domain/
COPY Notification.Infrastructure/Notification.Infrastructure.csproj Notification.Infrastructure/
# Restore dependencies
RUN dotnet restore Notification.WebAPI/Notification.WebAPI.csproj
# Copy the rest of source code
COPY . .
# Build & publish
WORKDIR /src/Notification.WebAPI
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

ENTRYPOINT ["dotnet", "Notification.WebAPI.dll"]
