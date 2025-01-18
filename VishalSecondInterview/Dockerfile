FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set Working directory
WORKDIR /app

# Copy the .csproj file
COPY *.csproj ./

# Restore dependencies
RUN dotnet restore

# Copy applicatin code
COPY . ./

# Build app
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Set working directory
WORKDIR /app/exoplanet

# Copy application code
COPY --from=build /app/out .

# Set entry point
ENTRYPOINT ["dotnet", "VishalSecondInterview.dll"]
