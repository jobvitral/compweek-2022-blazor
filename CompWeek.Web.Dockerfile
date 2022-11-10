FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /compWeek-web

# We copy the .csproj of our app to root and 
# restore the dependencies of the project.
COPY ["CompWeek.Web/CompWeek.Web.csproj", "CompWeek.Web/"]
RUN dotnet restore "CompWeek.Web/CompWeek.Web.csproj"

# We proceed by copying all the contents in
# the main project folder to root and build it
COPY . .
WORKDIR "/compWeek-web/CompWeek.Web"
RUN dotnet build "CompWeek.Web.csproj" -c Release -o /build

# Once we're done building, we'll publish the project
# to the publish folder 
FROM build AS publish
RUN dotnet publish "CompWeek.Web.csproj" -c Release -o /publish

# We then get the base image for Nginx and set the 
# work directory 
FROM nginx:1.17.1-alpine
WORKDIR /usr/share/nginx/html

# We'll copy all the contents from wwwroot in the publish
# folder into nginx/html for nginx to serve. The destination
# should be the same as what you set in the nginx.conf.
COPY --from=publish /publish/wwwroot /usr/local/webapp/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf