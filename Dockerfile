FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BeaverTinder.API/BeaverTinder.API.csproj", "BeaverTinder.API/"]
COPY ["BeaverTinder.Infrastructure/BeaverTinder.Infrastructure.csproj", "BeaverTinder.Infrastructure/"]
COPY ["BeaverTinder.Domain/BeaverTinder.Domain.csproj", "BeaverTinder.Domain/"]
COPY ["BeaverTinder.Application/BeaverTinder.Application.csproj", "BeaverTinder.Application/"]
RUN dotnet restore "BeaverTinder.API/BeaverTinder.API.csproj"
COPY . .
WORKDIR "/src/BeaverTinder.API"
RUN dotnet build "BeaverTinder.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BeaverTinder.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BeaverTinder.API.dll"]
