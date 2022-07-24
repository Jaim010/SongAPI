namespace Song.API.PostgreSQL
{
  public static class DbInitializer
  {
    public static void CreateDbIfNotExists(IHost host)
    {
      using (var scope = host.Services.CreateScope())
      {
        var services = scope.ServiceProvider;
        try
        {
          var context = services.GetRequiredService<SongContext>();
          Initialize(context);
        }
        catch (Exception ex)
        {
          var logger = services.GetRequiredService<ILogger<Program>>();
          logger.LogError(ex, "An error occurred creating the DB.");
        }
      }

    }

    private static void Initialize(SongContext context)
    {
      context.Database.EnsureCreated();

      // Look for any songs.
      if (context.Songs.Any())
        return;

      Models.Song[] songs = new Models.Song[] {
        new() {
          Name = "Baba Yaga",
          Artist = "Slaughter To Prevail",
          ImageUrl = "https://i1.sndcdn.com/artworks-dvaQQH6JZARF-0-t500x500.jpg",
        },
        new() {
          Name = "Breaking The Habit",
          Artist = "Linkin Park",
          ImageUrl = "https://www.bol.com/nl/nl/p/meteora/1000004004289106/",
        },
        new() {
          Name = "What The Dead Man Say",
          Artist = "Trivium",
          ImageUrl = "https://www.amazon.nl/Trivium-What-Dead-Men-Say/dp/B0851LYSHT",
        },
      };

      context.AddRange(songs);
      context.SaveChanges();
    }
  }
}