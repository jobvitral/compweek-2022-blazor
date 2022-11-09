FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /viravenda-api

COPY ["ViraVenda.Api/ViraVenda.Api.csproj", "ViraVenda.Api/"]
RUN dotnet restore "ViraVenda.Api/ViraVenda.Api.csproj"

COPY . .
WORKDIR "/viravenda-api/ViraVenda.Api"
RUN dotnet build "ViraVenda.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ViraVenda.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ViraVenda.Api.dll"]