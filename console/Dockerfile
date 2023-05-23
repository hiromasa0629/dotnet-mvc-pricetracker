FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS build
WORKDIR /app

COPY console.csproj .
RUN dotnet restore console.csproj

COPY . .
RUN dotnet publish -c