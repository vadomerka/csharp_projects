# PaymentsService.Web/Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PaymentsService.Web/PaymentsService.Web.csproj", "PaymentsService.Web/"]
COPY ["PaymentsService.Infrastructure/PaymentsService.Infrastructure.csproj", "PaymentsService.Infrastructure/"]
COPY ["PaymentsService.Entities/PaymentsService.Entities.csproj", "PaymentsService.Entities/"]
COPY ["PaymentsService.UseCases/PaymentsService.UseCases.csproj", "PaymentsService.UseCases/"]
RUN dotnet restore "PaymentsService.Web/PaymentsService.Web.csproj"
COPY . .
WORKDIR "/src/PaymentsService.Web"
RUN dotnet build "PaymentsService.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaymentsService.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentsService.Web.dll"]