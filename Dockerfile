# Use the official ASP.NET Core runtime image for .NET 8 as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80 443

# Use the official .NET SDK image for .NET 8 as the build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BSDigital.csproj", "./"]
RUN dotnet restore "./BSDigital.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "BSDigital.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BSDigital.csproj" -c Release -o /app/publish

# Build the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BSDigital.dll"]
