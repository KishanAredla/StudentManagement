# -------- BUILD STAGE --------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore first (better caching)
COPY StudentApi.csproj ./
RUN dotnet restore StudentApi.csproj

# Copy the rest of the source
COPY . ./
RUN dotnet publish StudentApi.csproj -c Release -o /app/publish

# -------- RUNTIME STAGE --------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .
EXPOSE 8080

ENTRYPOINT ["dotnet", "StudentApi.dll"]