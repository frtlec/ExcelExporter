FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY BlazorWebUI/*.csproj BlazorWebUI/ 
RUN dotnet restore  BlazorWebUI/*.csproj
COPY . .
RUN dotnet publish BlazorWebUI/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","BlazorWebUI.dll" ]
