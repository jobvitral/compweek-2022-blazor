FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /viravenda-identity

COPY ["ViraVenda.Identity/ViraVenda.Identity.csproj", "ViraVenda.Identity/"]
RUN dotnet restore "ViraVenda.Identity/ViraVenda.Identity.csproj"

COPY . .
WORKDIR "/viravenda-identity/ViraVenda.Identity"
RUN dotnet build "ViraVenda.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ViraVenda.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ViraVenda.Identity.dll"]