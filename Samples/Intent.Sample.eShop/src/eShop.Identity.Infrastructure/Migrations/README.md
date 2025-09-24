# Entity Framework Core migrations readme

See <https://learn.microsoft.com/ef/core/managing-schemas/migrations> for information about migrations
using Entity Framework Core.

You can perform these commands in the Visual Studio IDE using the Package Manager Console (VS PMC), which can
be opened through the `View` > `Other Windows` > `Package Manager Console` menu option, or using the .NET
Command Line Interface (.NET CLI) tools.

Substitute the curly brace (`{}`) enclosed arguments below with the appropriate migration name when
executing these commands.

## Visual Studio Package Manager Console quick reference

### Create a new migration (VS PMC)

```powershell
Add-Migration -Name {ChangeName} -StartupProject "eShop.Identity.Api" -Project "eShop.Identity.Infrastructure"
```

### Update the schema to the latest version (VS PMC)

```powershell
Update-Database -StartupProject "eShop.Identity.Api" -Project "eShop.Identity.Infrastructure"
```

## .NET CLI quick reference

### Create a new migration (.NET CLI)

```powershell
dotnet ef migrations add {ChangeName} --startup-project "eShop.Identity.Api" --project "eShop.Identity.Infrastructure"
```

### Update the schema to the latest version (.NET CLI)

```powershell
dotnet ef database update --startup-project "eShop.Identity.Api" --project "eShop.Identity.Infrastructure"
```

## Visual Studio Package Manager Console additional commands

### Generate a script which detects the current database schema version and updates it to the latest (VS PMC)

```powershell
Script-Migration -Idempotent -StartupProject "eShop.Identity.Api" -Project "eShop.Identity.Infrastructure"
```

### Generate a script which upgrades from and to a specific schema version (VS PMC)

```powershell
Script-Migration {Source} {Target} -StartupProject "eShop.Identity.Api" -Project "eShop.Identity.Infrastructure"
```

### Upgrade/downgrade the schema to a specific version (VS PMC)

```powershell
Update-Database -Migration {Target} -StartupProject "eShop.Identity.Api" -Project "eShop.Identity.Infrastructure"
```

### Remove the last created migration (VS PMC)

```powershell
Remove-Migration -StartupProject "eShop.Identity.Api" -Project "eShop.Identity.Infrastructure"
```

## .NET CLI additional commands

### Generate a script which detects the current database schema version and updates it to the latest (.NET CLI)

```powershell
dotnet ef migrations script --idempotent --startup-project "eShop.Identity.Api" --project "eShop.Identity.Infrastructure"
```

### Generate a script which upgrades from and to a specific schema version (.NET CLI)

```powershell
dotnet ef migrations script {Source} {Target} --startup-project "eShop.Identity.Api" --project "eShop.Identity.Infrastructure"
```

### Upgrade/downgrade the schema to a specific version (.NET CLI)

```powershell
dotnet ef database update {Target} --startup-project "eShop.Identity.Api" --project "eShop.Identity.Infrastructure"
```

### Remove the last created migration (.NET CLI)

```powershell
dotnet ef migrations remove --startup-project "eShop.Identity.Api" --project "eShop.Identity.Infrastructure"
```

## Drop all tables in a schema

```sql
DECLARE @SCHEMA AS varchar(max) = 'eShop.Identity'
DECLARE @EXECUTE_STATEMENT AS varchar(max) = (SELECT STUFF((SELECT CHAR(13) + CHAR(10) + [Statement] FROM (
    SELECT 'ALTER TABLE ['+@SCHEMA+'].['+[t].[name]+'] DROP CONSTRAINT ['+[fk].[name]+']' AS [Statement] FROM [sys].[foreign_keys] AS [fk] INNER JOIN [sys].[tables] AS [t] ON [t].[object_id] = [fk].[parent_object_id] INNER JOIN [sys].[schemas] AS [s] ON [s].[schema_id] = [t].[schema_id] WHERE [s].[name] = @SCHEMA
    UNION ALL
    SELECT 'DROP TABLE ['+@SCHEMA+'].['+[t].[name]+']' AS [Statement] FROM [sys].[tables] AS [t] INNER JOIN [sys].[schemas] AS [s] ON [s].[schema_id] = [t].[schema_id] WHERE [s].[name] = @SCHEMA
) A FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, ''))
EXECUTE(@EXECUTE_STATEMENT)
```
