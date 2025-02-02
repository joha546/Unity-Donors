# Use the .NET Framework SDK image for building the application (Windows-based image).
FROM mcr.microsoft.com/dotnet/framework/sdk:4.7.2-windowsservercore-ltsc2019 AS build

# Set the working directory to /src inside the container.
WORKDIR /src

# Copy the UnityDonors.csproj from the correct directory in the host to the container.
# Adjust the path to reflect the project structure.
COPY ["Application/UnityDonors/UnityDonors/UnityDonors.csproj", "UnityDonors/"]

# Run nuget restore to restore dependencies.
RUN nuget restore "UnityDonors/UnityDonors.csproj"

# Copy the rest of the project files into the container.
COPY . .

# Build the project in Release mode.
RUN msbuild "UnityDonors/UnityDonors.csproj" /p:Configuration=Release

# Publish the project to the specified output folder (adjust publish location).
RUN dotnet publish "UnityDonors/UnityDonors.csproj" -c Release -o G:/Unity-Donors/Publish

# Specify the base image for running the app (Windows-based image).
FROM mcr.microsoft.com/dotnet/framework/aspnet:4.7.2-windowsservercore-ltsc2019

# Set the working directory to the publish output folder.
WORKDIR /app

# Copy the published files from the build stage to the final container.
COPY --from=build G:/Unity-Donors/Publish .

# Expose port 80 for the container.
EXPOSE 80

# Set the entry point to run the application.
ENTRYPOINT ["C:\\inetpub\\wwwroot\\UnityDonors.exe"]
