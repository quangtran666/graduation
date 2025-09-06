PROJECT_PATH = src/App.Infrastructure
STARTUP_PROJECT = src/App.Api
OUTPUT_DIR = Data/Migrations

db-add:
	dotnet ef migrations add $(name) --project $(PROJECT_PATH) --startup-project $(STARTUP_PROJECT) --output-dir $(OUTPUT_DIR)

db-update:
	dotnet ef database update --project $(PROJECT_PATH) --startup-project $(STARTUP_PROJECT)

db-remove:
	dotnet ef migrations remove --project $(PROJECT_PATH) --startup-project $(STARTUP_PROJECT)

db-list:
	dotnet ef migrations list --project $(PROJECT_PATH) --startup-project $(STARTUP_PROJECT)