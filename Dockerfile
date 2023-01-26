#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["minimal_api_plus_razor.csproj", "."]
RUN dotnet restore "./minimal_api_plus_razor.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "minimal_api_plus_razor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "minimal_api_plus_razor.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "minimal_api_plus_razor.dll"]