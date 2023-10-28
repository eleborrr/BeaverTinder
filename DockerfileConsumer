FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SupportChat/SupportChat.csproj", "SupportChat/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "SupportChat/SupportChat.csproj"
COPY . .
WORKDIR "/src/SupportChat"
RUN dotnet build "SupportChat.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SupportChat.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SupportChat.dll"]
