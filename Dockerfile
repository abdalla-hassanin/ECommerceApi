# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-dev
WORKDIR /app

# Copy all .csproj files and restore as distinct layers
COPY ["ECommerceApi.Api/ECommerceApi.Api.csproj", "ECommerceApi.Api/"]
COPY ["ECommerceApi.Core/ECommerceApi.Core.csproj", "ECommerceApi.Core/"]
COPY ["ECommerceApi.Data/ECommerceApi.Data.csproj", "ECommerceApi.Data/"]
COPY ["ECommerceApi.Infrastructure/ECommerceApi.Infrastructure.csproj", "ECommerceApi.Infrastructure/"]
COPY ["ECommerceApi.Service/ECommerceApi.Service.csproj", "ECommerceApi.Service/"]

RUN dotnet restore "ECommerceApi.Api/ECommerceApi.Api.csproj"
RUN dotnet restore "ECommerceApi.Core/ECommerceApi.Core.csproj"
RUN dotnet restore "ECommerceApi.Data/ECommerceApi.Data.csproj"
RUN dotnet restore "ECommerceApi.Infrastructure/ECommerceApi.Infrastructure.csproj"
RUN dotnet restore "ECommerceApi.Service/ECommerceApi.Service.csproj"

# Copy everything else
COPY . ./
 
# Publish the application
RUN dotnet publish -c release -o out

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 80
COPY --from=build-dev /app/out . 
ENTRYPOINT ["dotnet", "ECommerceApi.Api.dll"]