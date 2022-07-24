using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using Song.API.PostgreSQL;
using Song.API.Models;

namespace Song.API.UnitTests
{
  public abstract class SongDataSeed : IDisposable
  {
    protected static DbContextOptions<SongContext> dbContextOptions = new DbContextOptionsBuilder<SongContext>()
      .UseInMemoryDatabase(databaseName: "InMemoryDbForTesting")
      .UseQueryTrackingBehavior(queryTrackingBehavior: QueryTrackingBehavior.NoTracking)
      .Options;

    protected SongContext context;

    // Called before every test method.
    protected SongDataSeed()
    {
      context = new SongContext(dbContextOptions);
      context.Database.EnsureCreated();

      SeedDatabase();
    }

    // Called after every test method.
    public void Dispose() => context.Database.EnsureDeleted();

    public async void SeedDatabase()
    {
      if (await context.Songs.CountAsync() == 0)
      {
        var songs = new List<Models.Song> {
          new() { Id=1, Name="The Dying Song", Artist="Slipknot", ImageUrl=""},
          new() { Id=2, Name="Faint", Artist="Linkin Park", ImageUrl=""},
          new() { Id=3, Name="Shogun", Artist="Trivium", ImageUrl=""},
          new() { Id=4, Name="Like You Do", Artist="Joji", ImageUrl=""},
        };
        context.Songs.AddRange(songs);
        context.SaveChanges();
      }
    }
  }
}