######## Requirements. Command 'make' needs to be installed.

# Install Chocolately, a Package Manager for Windows, to get 'make' command available (can be omitted if make exists already).
# https://chocolatey.org/install

# Then, install 'make' command via Chocolately.
# choco install make

######## Variables Definition.
UserName := SA
UserPassword := "<YourStrong@Passw0rd>"
ContainerName := sql1
ServerName := localhost
ServiceName := com.docker.service
CommandPath := /opt/mssql-tools/bin/sqlcmd

DatabaseName := FoodMenu
MSSQLPath := /var/opt/mssql/
BackupFile := backup.bak

StartupProject := ../FoodMenu-RazorPages
DataAccessProject := ../FoodMenu.DataAccess

# Definition: Get Arguments, after getting first command.
RUN_ARGS := $(wordlist 2,$(words $(MAKECMDGOALS)),$(MAKECMDGOALS))

# Commands Definition:
# % = Rule that will match any task name.
# @ = Argument data.
%:
	@ECHO Command not found, using as Argument: "$(@)".

commands:
	@ECHO === DOTNET shortcuts available ===
	@ECHO dotnet_restore: Restore tools defined in manifest file.
	@ECHO dotnet_watch: Quick reload for $(StartupProject) project.
	
	@ECHO === DOCKER shortcuts available ===
	@ECHO docker_run: Start Container.
	@ECHO docker_service: Start Docker Service.
	@ECHO docker_sql: Execute SQL commands, if provided, then get into SQL cmd.

	@ECHO === Entity Frameworks shortcuts available ===
	@ECHO db_drop: Drop DB.
	@ECHO db_update: Update DB based on latest defined Migrations.
	@ECHO db_add: Create new Migration with latest changes, require an argument to be used as Migration Name.

######## Docker commands execution. 

docker_service:
	@ECHO Enable Docker Service (Windows Only).
	NET start $(ServiceName)
	
docker_run:
	@ECHO Run Docker.
	DOCKER start $(ContainerName)

docker_sql: docker_run
	@ECHO Connect to MSSQL and execute query if arguments provided.
	DOCKER exec -it $(ContainerName) $(CommandPath) -S $(ServerName) -U $(UserName) -P $(UserPassword) -d $(DatabaseName) -Q "$(RUN_ARGS)"

docker_backup:
	@ECHO Creating DB backup.
	DOCKER exec -it $(ContainerName) $(CommandPath) -b -V16 -S $(ServerName) -U $(UserName) -P $(UserPassword) -Q "BACKUP DATABASE [$(DatabaseName)] TO DISK = N'$(MSSQLPath)backups/$(BackupFile)' with NOFORMAT, NOINIT, NAME = '$(DatabaseName)-full', SKIP, NOREWIND, NOUNLOAD, STATS = 10"
	DOCKER cp $(ContainerName):$(MSSQLPath)backups/$(BackupFile) ./$(BackupFile)

docker_restore:
	@ECHO Restoring DB backup.
	docker exec $(ContainerName) mkdir -p $(MSSQLPath)/restores
	docker cp "./$(BackupFile)" "$(ContainerName):$(MSSQLPath)restores/$(BackupFile)"
	docker exec -it $(ContainerName) $(CommandPath) -S $(ServerName) -U $(UserName) -Q "RESTORE DATABASE [$(DatabaseName)] FROM DISK = N'$(MSSQLPath)/restores/$(BackupFile)' WITH FILE = 1, NOUNLOAD, REPLACE, RECOVERY, STATS = 5"

######## Dotnet commands execution.
dotnet_watch:
	@ECHO Quick-reload Project.
	dotnet watch --project $(StartupProject) run

dotnet_restore:
	@ECHO Restore and Install dotnet-ef in order to help with EF Core.
	DOTNET tool restore

######## Entity Framework commands execution.
db_update:
	@ECHO Update Database based on Migrations.
	DOTNET dotnet-ef Database Update -p $(DataAccessProject) -s $(StartupProject)

db_drop:
	@ECHO Drop DB.
	DOTNET dotnet-ef Database Drop -p $(DataAccessProject) -s $(StartupProject)

db_add:
	@ECHO Add Migrations.
	DOTNET dotnet-ef Migrations Add $(RUN_ARGS) -p $(DataAccessProject) -s $(StartupProject)