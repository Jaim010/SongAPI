# .NET 6, ASP.NET Core & Entity Framework Core - Song API

## Setup
The default connection string should be set before running. Replace each field with your own data. This format follows the Npgsql string format for PostgreSQL.<br />
```
...
"ConnectionStrings": {
    "DefaultConnection": "Server=127.0.0.1;Port=5432;Database=myDataBase;User Id=myUsername;Password=myPassword;"
  }
...
```
## Running
Run the command below in the `/song-api/` directory. <br />
`dotnet run`
