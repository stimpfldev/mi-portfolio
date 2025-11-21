# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia todo el repo
COPY . .

# Cambia al directorio donde está el .csproj
WORKDIR /src/PersonalWeb

# Publica el proyecto
RUN dotnet publish -c Release -o /app

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
ENTRYPOINT ["dotnet", "PersonalWeb.dll"]
