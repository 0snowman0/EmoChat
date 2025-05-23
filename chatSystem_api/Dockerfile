# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["chatSystem_api/chatSystem_api.csproj", "chatSystem_api/"]
COPY ["Event-Driven/RabbitMQEventBus/RabbitMQEventBus.csproj", "Event-Driven/RabbitMQEventBus/"]
COPY ["src/ChatSystem/ChatSystem_Application/ChatSystem_Application.csproj", "src/ChatSystem/ChatSystem_Application/"]
COPY ["src/ChatSystem/ChatSystem_Domain/ChatSystem_Domain.csproj", "src/ChatSystem/ChatSystem_Domain/"]
COPY ["src/ChatSystem/ChatSystem_persistence/ChatSystem_persistence.csproj", "src/ChatSystem/ChatSystem_persistence/"]
RUN dotnet restore "./chatSystem_api/chatSystem_api.csproj"
COPY . .
WORKDIR "/src/chatSystem_api"
RUN dotnet build "./chatSystem_api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./chatSystem_api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "chatSystem_api.dll"]