# Use official image as base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["todo.API/todo.API.csproj", "todo.API/"]
RUN dotnet restore "todo.API/todo.API.csproj"
COPY . .
WORKDIR "/src/todo.API"
RUN dotnet build "todo.API.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "todo.API.csproj" -c Release -o /app/publish

# Final stage (run app)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "todo.API.dll"]
