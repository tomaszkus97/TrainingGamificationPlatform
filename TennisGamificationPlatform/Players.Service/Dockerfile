FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Players.Service/Players.Service.csproj", "Players.Service/"]
RUN dotnet restore "Players.Service/Players.Service.csproj"
COPY . .
WORKDIR "/src/Players.Service"
RUN dotnet build "Players.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Players.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Players.Service.dll"]