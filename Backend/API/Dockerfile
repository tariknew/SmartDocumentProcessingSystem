# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . .
RUN dotnet restore API/API.csproj
RUN dotnet publish API/API.csproj -c Release -o /out

# Stage 2: Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
EXPOSE 5055
ENV ASPNETCORE_URLS=http://+:5055
COPY --from=build /out .

EXPOSE 80
ENTRYPOINT ["dotnet", "API.dll"]