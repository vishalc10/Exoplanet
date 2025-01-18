## Running the Project
To run this Exoplanet project locally, follow these steps:

### 1. **Clone the Repository**
```bash
git clone https://github.com/vishalc10/Exoplanet.git
```
### 2. Navigate to the Project Directory
```bash
cd VishalSecondInterview
```
### 3.  Install Prerequisites
Make sure you have .NET SDK
```bash
dotnet --version
```
### 4. Restore Project Dependencies
Before running the application, restore the required NuGet dependencies by running:
```bash
dotnet restore
```
This will download the necessary packages as defined in the project’s csproj file.

### 5. Run the Application
Once the dependencies are restored, you can run the application with the following command:
```bash
dotnet run
```

## Run Unit Tests

### 1. Navigate to the Project Directory
```bash
cd ExoplanetTests
```

### 2. Run the tests
```bash
dotnet test
```
This will run all the unit tests in the project and provide feedback on whether any tests have failed.

## Dockerfile

### 1. Navigate to the Project Directory
```bash
cd VishalSecondInterview
```
### 2. Build docker image
```bash
docker build -t exoplanet .
```
###3. Run the container
```bash
docker run --rm exoplanet
```
