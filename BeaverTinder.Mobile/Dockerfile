FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BeaverTinder.Mobile/BeaverTinder.Mobile.csproj", "BeaverTinder.Mobile/"]
RUN dotnet restore "BeaverTinder.Mobile/BeaverTinder.Mobile.csproj"
COPY . .
WORKDIR "/src/BeaverTinder.Mobile"
RUN dotnet build "BeaverTinder.Mobile.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BeaverTinder.Mobile.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BeaverTinder.Mobile.dll"]
