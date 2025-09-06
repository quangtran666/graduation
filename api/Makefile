include scripts/database.mk
include scripts/api.mk

.DEFAULT_GOAL := help

help:
	@echo "=== API Commands ==="
	@echo ""
	@echo "Database:"
	@echo "  make db-add name=<migration-name>  - Add migration"
	@echo "  make db-update                     - Update database"
	@echo "  make db-remove                     - Remove last migration"
	@echo "  make db-list                       - List migrations"
	@echo ""
	@echo "API:"
	@echo "  make api-run                       - Run API"
	@echo "  make api-watch                     - Run with hot reload"
	@echo "  make api-build                     - Build solution"
	@echo "  make api-clean                     - Clean solution"
	@echo "  make api-restore                   - Restore packages"