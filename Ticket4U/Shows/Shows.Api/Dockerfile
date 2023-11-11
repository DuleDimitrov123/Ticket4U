#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Shows/Shows.Api/Shows.Api.csproj", "Shows/Shows.Api/"]
COPY ["Shared/Shared.Api/Shared.Api.csproj", "Shared/Shared.Api/"]
COPY ["Shared/Shared.Infrastructure/Shared.Infrastructure.csproj", "Shared/Shared.Infrastructure/"]
COPY ["Shared/Shared.Application/Shared.Application.csproj", "Shared/Shared.Application/"]
COPY ["Shared/Shared.Domain/Shared.Domain.csproj", "Shared/Shared.Domain/"]
COPY ["Shows/Shows.Infrastructure/Shows.Infrastructure.csproj", "Shows/Shows.Infrastructure/"]
COPY ["Shows/Shows.Application/Shows.Application.csproj", "Shows/Shows.Application/"]
COPY ["Shows/Shows.Domain/Shows.Domain.csproj", "Shows/Shows.Domain/"]
COPY ["Common/Shows.Common.csproj", "Common/"]
RUN dotnet restore "./Shows/Shows.Api/./Shows.Api.csproj"
COPY . .
WORKDIR "/src/Shows/Shows.Api"
RUN dotnet build "./Shows.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Shows.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shows.Api.dll"]