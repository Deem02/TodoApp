# TodoApp API

RESTful To-Do List API built with ASP.NET Core 8, Entity Framework Core 8, and PostgreSQL.

## Requirements

- .NET 8 SDK
- PostgreSQL (running locally)
- EF Core CLI tool:
    
    ```bash
    dotnet tool install --global dotnet-ef --version 8.0.34
    ```
    

## Project structure

| Project | Responsibility |
| --- | --- |
| `TodoApp.Domain` | Entities and enums. No dependencies |
| `TodoApp.Application` | Use cases, CQRS, DTOs, validators, interfaces (e.g. `IAppDbContext`) |
| `TodoApp.Infrastructure` | `AppDbContext`, EF Core configurations, migrations,  admin seeder |
| `TodoApp.API` | Controllers, `Program.cs`, Swagger |

## **Getting the code**

```c
git clone https://github.com/Deem02/TodoApp.git
cd TodoApp
```

## Connection string

`appsettings.json` contains a placeholder only. Set your real connection string using User Secrets:

Command line:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=TodoAppDb;Username=postgres;Password=<your-password>" --project TodoApp.API
```

Visual Studio: right-click `TodoApp.API`, choose **Manage User Secrets**, and add the same value in the JSON file that opens.

If you get an error about a missing `UserSecretsId`, run `dotnet user-secrets init --project TodoApp.API` once.

## Database migrations

Apply the existing migrations to create the database and tables:

```bash
dotnet ef database update --project TodoApp.Infrastructure --startup-project TodoApp.API
```

Add a new migration:

```bash
dotnet ef migrations add <MigrationName> --project TodoApp.Infrastructure --startup-project TodoApp.API --output-dir Persistence/Migrations
```

Visual Studio alternative (Package Manager Console): set **Default project** to `TodoApp.Infrastructure` and make sure `TodoApp.API` is the startup project, then run:

```c
Add-Migration <MigrationName> 
Update-Database
```

## Admin account

One Admin account is created automatically the first time the application starts, if no Admin exists yet. Set its email and password in User Secrets before the first run:

```c
dotnet user-secrets set "SeedAdmin:Email" "admin@todoapp.local" --project TodoApp.API
dotnet user-secrets set "SeedAdmin:Password" "<your-password>" --project TodoApp.API
```

In Visual Studio, the same values go in the User Secrets JSON file:

```c
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=TodoAppDb;Username=postgres;Password=<your-password>"
  },
  "SeedAdmin": {
    "Email": "admin@todoapp.local",
    "Password": "<your-password>"
  }
}
```

## Run

```bash
dotnet run --project TodoApp.API
```

Open Swagger at `https://localhost:<port>/swagger` The port is shown in the console output.