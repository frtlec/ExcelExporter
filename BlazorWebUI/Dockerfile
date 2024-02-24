FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy AS base
USER root
WORKDIR /app
EXPOSE 80
EXPOSE 443
FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BlazorWebUI/BlazorWebUI.csproj", "BlazorWebUI/"]
RUN dotnet restore "./BlazorWebUI/BlazorWebUI.csproj"
COPY . .
WORKDIR "/src/BlazorWebUI"
RUN dotnet build "./BlazorWebUI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BlazorWebUI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "BlazorWebUI.dll"]