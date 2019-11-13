FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Identity.Service/Identity.Service.csproj", "Identity.Service/"]
RUN dotnet restore "Identity.Service/Identity.Service.csproj"
COPY . .
WORKDIR "/src/Identity.Service"
RUN dotnet build "Identity.Service.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Identity.Service.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Identity.Service.dll"]