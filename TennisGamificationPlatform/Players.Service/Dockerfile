# Dockerfile
FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env

WORKDIR /app
COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY --from=build-env /app/out .
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT ["dotnet", "Players.Service.dll"]
