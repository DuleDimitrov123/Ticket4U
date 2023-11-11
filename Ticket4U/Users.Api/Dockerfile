#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Users.Api/Users.Api.csproj", "Users.Api/"]
COPY ["Shared.Api/Shared.Api.csproj", "Shared.Api/"]
COPY ["Shared.Application/Shared.Application.csproj", "Shared.Application/"]
COPY ["Shared.Domain/Shared.Domain.csproj", "Shared.Domain/"]
COPY ["Shared.Infrastructure/Shared.Infrastructure.csproj", "Shared.Infrastructure/"]
COPY ["Users.Application/Users.Application.csproj", "Users.Application/"]
COPY ["Users.Domain/Users.Domain.csproj", "Users.Domain/"]
COPY ["Users.Common/Users.Common.csproj", "Users.Common/"]
COPY ["Users.Infrastructure/Users.Infrastructure.csproj", "Users.Infrastructure/"]
RUN dotnet restore "./Users.Api/./Users.Api.csproj"
COPY . .
WORKDIR "/src/Users.Api"
RUN dotnet build "./Users.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Users.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Users.Api.dll"]