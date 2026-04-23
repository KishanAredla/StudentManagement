# -------- BUILD STAGE --------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore first (better caching)
COPY Student.Web.csproj ./
RUN dotnet restore Student.Web.csproj

# Copy the rest of the source
COPY . ./
RUN dotnet publish Student.Web.csproj -c Release -o /app/publish

# -------- RUNTIME STAGE --------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .
EXPOSE 8080

ENTRYPOINT ["dotnet", "Student.Web.dll"]