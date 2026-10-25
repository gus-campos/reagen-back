# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Restore em camada separada para aproveitar cache
COPY ReagenBack.Core/ReagenBack.Core.csproj ReagenBack.Core/
RUN dotnet restore ReagenBack.Core/ReagenBack.Core.csproj

COPY ReagenBack.Core/ ReagenBack.Core/
RUN dotnet publish ReagenBack.Core/ReagenBack.Core.csproj -c Release -o /app --no-restore

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .

ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

USER app
ENTRYPOINT ["dotnet", "ReagenBack.Core.dll"]
