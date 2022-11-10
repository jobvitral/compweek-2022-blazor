FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /compWeek-identity

COPY ["CompWeek.Identity/CompWeek.Identity.csproj", "CompWeek.Identity/"]
RUN dotnet restore "CompWeek.Identity/CompWeek.Identity.csproj"

COPY . .
WORKDIR "/compWeek-identity/CompWeek.Identity"
RUN dotnet build "CompWeek.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CompWeek.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CompWeek.Identity.dll"]