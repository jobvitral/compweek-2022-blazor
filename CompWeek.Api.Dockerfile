FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /compWeek-api

COPY ["CompWeek.Api/CompWeek.Api.csproj", "CompWeek.Api/"]
RUN dotnet restore "CompWeek.Api/CompWeek.Api.csproj"

COPY . .
WORKDIR "/compWeek-api/CompWeek.Api"
RUN dotnet build "CompWeek.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CompWeek.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CompWeek.Api.dll"]