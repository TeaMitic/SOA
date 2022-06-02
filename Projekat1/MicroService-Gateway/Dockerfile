FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5170

ENV ASPNETCORE_URLS=http://+:5170
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MicroService-Gateway.csproj", "./"]
RUN dotnet restore "MicroService-Gateway.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "MicroService-Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroService-Gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroService-Gateway.dll"]
