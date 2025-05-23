# Set the base image
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS base
WORKDIR /app
EXPOSE 5039

# Add the SDK so you can run the dotnet restore and build commands
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY --link *.csproj ./
RUN dotnet restore "BackendApi.csproj"
COPY --link . .
RUN dotnet build "BackendApi.csproj" -c Release -o /app/build

# Create the publish files
FROM build AS publish
RUN dotnet publish "BackendApi.csproj" -c Release -o /app/publish

# Copy the publish files into the container
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BackendApi.dll"]

