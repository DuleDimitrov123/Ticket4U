#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Reservations.Api/Reservations.Api.csproj", "Reservations.Api/"]
COPY ["Reservations.Application/Reservations.Application.csproj", "Reservations.Application/"]
COPY ["Reservations.Domain/Reservations.Domain.csproj", "Reservations.Domain/"]
COPY ["Reservations.Common/Reservations.Common.csproj", "Reservations.Common/"]
COPY ["Shared.Domain/Shared.Domain.csproj", "Shared.Domain/"]
COPY ["Shared.Application/Shared.Application.csproj", "Shared.Application/"]
COPY ["Reservations.Infrastructure/Reservations.Infrastructure.csproj", "Reservations.Infrastructure/"]
COPY ["Shared.Infrastructure/Shared.Infrastructure.csproj", "Shared.Infrastructure/"]
COPY ["Shared.Api/Shared.Api.csproj", "Shared.Api/"]
RUN dotnet restore "./Reservations.Api/./Reservations.Api.csproj"
COPY . .
WORKDIR "/src/Reservations.Api"
RUN dotnet build "./Reservations.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Reservations.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Reservations.Api.dll"]