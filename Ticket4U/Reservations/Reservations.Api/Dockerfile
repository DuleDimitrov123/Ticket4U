#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Reservations/Reservations.Api/Reservations.Api.csproj", "Reservations/Reservations.Api/"]
COPY ["Shared/Shared.Api/Shared.Api.csproj", "Shared/Shared.Api/"]
COPY ["Shared/Shared.Infrastructure/Shared.Infrastructure.csproj", "Shared/Shared.Infrastructure/"]
COPY ["Shared/Shared.Application/Shared.Application.csproj", "Shared/Shared.Application/"]
COPY ["Shared/Shared.Domain/Shared.Domain.csproj", "Shared/Shared.Domain/"]
COPY ["Reservations/Reservations.Infrastructure/Reservations.Infrastructure.csproj", "Reservations/Reservations.Infrastructure/"]
COPY ["Reservations/Reservations.Application/Reservations.Application.csproj", "Reservations/Reservations.Application/"]
COPY ["Reservations/Reservations.Domain/Reservations.Domain.csproj", "Reservations/Reservations.Domain/"]
COPY ["Reservations/Reservations.Common/Reservations.Common.csproj", "Reservations/Reservations.Common/"]
RUN dotnet restore "./Reservations/Reservations.Api/./Reservations.Api.csproj"
COPY . .
WORKDIR "/src/Reservations/Reservations.Api"
RUN dotnet build "./Reservations.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Reservations.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Reservations.Api.dll"]