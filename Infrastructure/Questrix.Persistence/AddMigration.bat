@echo off

set /p migrationName="Enter the migration name: "
dotnet ef migrations add %migrationName%