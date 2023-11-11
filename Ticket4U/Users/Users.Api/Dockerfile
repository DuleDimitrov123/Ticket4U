#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Users/Users.Api/Users.Api.csproj", "Users/Users.Api/"]
COPY ["Shared/Shared.Api/Shared.Api.csproj", "Shared/Shared.Api/"]
COPY ["Shared/Shared.Infrastructure/Shared.Infrastructure.csproj", "Shared/Shared.Infrastructure/"]
COPY ["Shared/Shared.Application/Shared.Application.csproj", "Shared/Shared.Application/"]
COPY ["Shared/Shared.Domain/Shared.Domain.csproj", "Shared/Shared.Domain/"]
COPY ["Users/Users.Infrastructure/Users.Infrastructure.csproj", "Users/Users.Infrastructure/"]
COPY ["Users/Users.Application/Users.Application.csproj", "Users/Users.Application/"]
COPY ["Users/Users.Domain/Users.Domain.csproj", "Users/Users.Domain/"]
COPY ["Users/Users.Common/Users.Common.csproj", "Users/Users.Common/"]
RUN dotnet restore "./Users/Users.Api/./Users.Api.csproj"
COPY . .
WORKDIR "/src/Users/Users.Api"
RUN dotnet build "./Users.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Users.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Users.Api.dll"]