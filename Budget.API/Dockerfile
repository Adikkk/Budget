FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Budget.API/Budget.API.csproj", "Budget.API/"]
COPY ["Budget.Application/Budget.Application.csproj", "Budget.Application/"]
COPY ["Budget.Domain/Budget.Domain.csproj", "Budget.Domain/"]
COPY ["Budget.Infrastructure/Budget.Infrastructure.csproj", "Budget.Infrastructure/"]
RUN dotnet restore "Budget.API/Budget.API.csproj"
COPY . .
WORKDIR "/src/Budget.API"
RUN dotnet build "Budget.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Budget.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Budget.API.dll"]