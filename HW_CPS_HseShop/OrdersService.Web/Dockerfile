# OrdersService.Web/Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["OrdersService.Web/OrdersService.Web.csproj", "OrdersService.Web/"]
COPY ["OrdersService.Infrastructure/OrdersService.Infrastructure.csproj", "OrdersService.Infrastructure/"]
COPY ["OrdersService.Entities/OrdersService.Entities.csproj", "OrdersService.Entities/"]
COPY ["OrdersService.UseCases/OrdersService.UseCases.csproj", "OrdersService.UseCases/"]
RUN dotnet restore "OrdersService.Web/OrdersService.Web.csproj"
COPY . .
WORKDIR "/src/OrdersService.Web"
RUN dotnet build "OrdersService.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrdersService.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrdersService.Web.dll"]