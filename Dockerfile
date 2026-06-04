FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["EventHub.csproj", "./"]
RUN dotnet restore "EventHub.csproj"

COPY . .
RUN dotnet publish "EventHub.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app

COPY --from=build /app/publish .
COPY --from=build /src/Views ./Views
COPY --from=build /src/wwwroot ./wwwroot

EXPOSE 8080
ENTRYPOINT ["dotnet", "EventHub.dll"]
