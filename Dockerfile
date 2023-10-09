FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BeaverTinder/BeaverTinder.csproj", "BeaverTinder/"]
COPY ["Infrastructure/Persistence.Misc/Persistence.Misc.csproj", "Infrastructure/Persistence.Misc/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Persistence/Persistence.csproj", "Persistence/"]
COPY ["Services.Abstraction/Services.Abstraction.csproj", "Services.Abstraction/"]
COPY ["Presentation/Presentation.csproj", "Presentation/"]
COPY ["Services/Services.csproj", "Services/"]
RUN dotnet restore "BeaverTinder/BeaverTinder.csproj"
COPY . .
WORKDIR "/src/BeaverTinder"
RUN dotnet build "BeaverTinder.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BeaverTinder.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BeaverTinder.dll"]
