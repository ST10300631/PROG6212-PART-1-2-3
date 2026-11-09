# Build Stage
FROM ://microsoft.com AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["RaceDaySystem.Client.csproj", "./"]
RUN dotnet restore "RaceDaySystem.Client.csproj"

# Copy the rest of the source code and build
COPY . .
RUN dotnet build "RaceDaySystem.Client.csproj" -c Release -o /app/build

# Publish Stage
FROM build AS publish
RUN dotnet publish "RaceDaySystem.Client.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime Stage
FROM ://microsoft.com AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
EXPOSE 8081

ENTRYPOINT ["dotnet", "RaceDaySystem.Client.dll"]
