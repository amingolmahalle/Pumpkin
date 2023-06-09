# EF Core Migrations
These command would help you yo setup EF in this project and use migrations to
change your database schema. The database engine is `sqlserver`, supported officially
by `.net core >= 6`.

## Check If EF is Enabled?
To enable EF in persistence project of your solution, should enter to project
path. As for this project is `Src/Infrastructure/Contexts` :
> cd Src/Infrastructure/Contexts

Now you can check EF is enabled in you project by this command 
>dotnet-ef --version

You should see EF core version in result, if it is installed, otherwise 
this message will be shown
```
Could not execute because the specified command or file was not found.
Possible reasons for this include:
  * You misspelled a built-in dotnet command.
  * You intended to execute a .NET program, but dotnet-ef does not exist.
  * You intended to run a global tool, but a dotnet-prefixed executable with this name could not be found on the PATH.
```

## Install EF
To install latest version of EF core, use `dotnet tool` command
```
dotnet tool install --global dotnet-ef
```
It's recommended to use `uninstall` command before using `install`, to prevent
old versions conflicts.
```
dotnet tool uninstall --global dotnet-ef
```

## Add Migrations

```
dotnet ef migrations add initialize_database --context SqlServerCommandDbContext --output-dir SqlServer/Migrations --startup-project=../../Presentation/ClientWebApi/ClientWebApi.csproj
```

## Update Database

```
dotnet ef database update --context SqlServerCommandDbContext --startup-project=../../Presentation/ClientWebApi/ClientWebApi.csproj

```
