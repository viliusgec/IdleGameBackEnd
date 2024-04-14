#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["IdleGame.Api.Host/IdleGame.Api.Host.csproj", "IdleGame.Api.Host/"]
COPY ["IdleGame.Api.Contracts/IdleGame.Api.Contracts.csproj", "IdleGame.Api.Contracts/"]
COPY ["IdleGame.ApplicationServices/IdleGame.ApplicationServices.csproj", "IdleGame.ApplicationServices/"]
COPY ["IdleGame.Domain/IdleGame.Domain.csproj", "IdleGame.Domain/"]
COPY ["IdleGame.Infrastructure/IdleGame.Infrastructure.csproj", "IdleGame.Infrastructure/"]
RUN dotnet restore "./IdleGame.Api.Host/IdleGame.Api.Host.csproj"
COPY . .
WORKDIR "/src/IdleGame.Api.Host"
RUN dotnet build "./IdleGame.Api.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./IdleGame.Api.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdleGame.Api.Host.dll"]