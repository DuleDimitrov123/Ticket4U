#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Ticket4U.ApiGateway/Ticket4U.ApiGateway.csproj", "Ticket4U.ApiGateway/"]
RUN dotnet restore "Ticket4U.ApiGateway/Ticket4U.ApiGateway.csproj"
COPY . .
WORKDIR "/src/Ticket4U.ApiGateway"
RUN dotnet build "Ticket4U.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ticket4U.ApiGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ticket4U.ApiGateway.dll"]