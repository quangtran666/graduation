STARTUP_PROJECT = src/App.Api
SOLUTION = api.sln

api-run:
	dotnet run --project $(STARTUP_PROJECT)

api-build:
	dotnet build $(SOLUTION)

api-clean:
	dotnet clean $(SOLUTION)

api-restore:
	dotnet restore $(SOLUTION)