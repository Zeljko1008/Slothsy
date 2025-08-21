# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#USER $APP_UID
WORKDIR /app
EXPOSE 8080

# -------------------------
# Build stage
# -------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# This stage is used to build the service project

COPY ["Slothsy.WebAPI/Slothsy.WebAPI.csproj", "Slothsy.WebAPI/"]
COPY ["Slothsy.Application/Slothsy.Application.csproj", "Slothsy.Application/"]
COPY ["Slothsy.Domain/Slothsy.Domain.csproj", "Slothsy.Domain/"]
COPY ["Slothsy.Common/Slothsy.Common.csproj", "Slothsy.Common/"]
COPY ["Slothsy.Infrastructure/Slothsy.Infrastructure.csproj", "Slothsy.Infrastructure/"]

RUN dotnet restore "./Slothsy.WebAPI/Slothsy.WebAPI.csproj"
COPY . .
WORKDIR "/src/Slothsy.WebAPI"

# This stage is used to publish the service project to be copied to the final stage

RUN dotnet publish "./Slothsy.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .



# Environment variables for Kestrel
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER=0 

ENTRYPOINT ["dotnet", "Slothsy.WebAPI.dll"]
