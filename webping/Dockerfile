FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
ENV TARGET "https://www.google.co.in/"
ENV INTERVAL 3000
ENV METHOD HEAD


WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet","webping.dll"]