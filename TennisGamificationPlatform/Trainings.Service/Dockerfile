FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Trainings.Service/Trainings.Service.csproj", "Trainings.Service/"]
RUN dotnet restore "Trainings.Service/Trainings.Service.csproj"
COPY . .
WORKDIR "/src/Trainings.Service"
RUN dotnet build "Trainings.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Trainings.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Trainings.Service.dll"]