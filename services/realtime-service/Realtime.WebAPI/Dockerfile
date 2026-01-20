# =========================
# 1. Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Copy csproj files (optimize restore cache)
COPY Realtime.WebAPI/Realtime.WebAPI.csproj Realtime.WebAPI/
COPY Realtime.Application/Realtime.Application.csproj Realtime.Application/
COPY Realtime.Domain/Realtime.Domain.csproj Realtime.Domain/
COPY Realtime.Infrastructure/Realtime.Infrastructure.csproj Realtime.Infrastructure/
# Restore dependencies
RUN dotnet restore Realtime.WebAPI/Realtime.WebAPI.csproj
# Copy the rest of source code
COPY . .
# Build & publish
WORKDIR /src/Realtime.WebAPI
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

ENTRYPOINT ["dotnet", "Realtime.WebAPI.dll"]
