FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BeaverTinder/BeaverTinder.csproj", "BeaverTinder/"]
RUN dotnet restore "BeaverTinder/BeaverTinder.csproj"
COPY . .
WORKDIR "/src/BeaverTinder"
RUN dotnet build "BeaverTinder.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BeaverTinder.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BeaverTinder.dll"]
