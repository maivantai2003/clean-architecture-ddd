# =========================
# 1. Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Copy csproj trước để tận dụng cache
COPY APIGateway.csproj ./
RUN dotnet restore
# Copy toàn bộ source
COPY . .
# Publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false
# =========================
# 2. Runtime stage
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 8080
# Copy output từ build stage
COPY --from=build /app/publish .
# Environment
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "APIGateway.dll"]
