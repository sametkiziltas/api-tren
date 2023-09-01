FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7183
EXPOSE 5275

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/tren.api/tren.api.csproj", "src/tren.api/"]
RUN dotnet restore "src/tren.api/tren.api.csproj"
COPY . .
WORKDIR "/src/src/tren.api"
RUN dotnet build "tren.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "tren.api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "tren.api.dll"]
