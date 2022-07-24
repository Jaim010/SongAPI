# .NET 6, ASP.NET Core & Entity Framework Core 6 - Song API

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

Run `dotnet run` in the `/Song.API/` directory. <br />

## Testing

Run `dotnet test` in the root directory (`/`). <br />

## Docker

Before we can build the Docker image, we have to build the Song.API project. To do so run `dotnet publish` in the `/Song.API/` directory. Now we can build the image. In the root directory (`/`) run `docker build .`.
