using Song.API.Models;
using Song.API.PostgreSQL;

namespace Song.API.IntergrationTests
{
  public class Utilities
  {
    public static void InitializeDbForTets(SongContext db)
    {
      db.Songs.AddRange(GetSeedingSongs());
      db.SaveChanges();
    }

    public static void ReinitializeDbForTests(SongContext db)
    {
      db.Songs.RemoveRange(db.Songs);
      InitializeDbForTets(db);
    }

    private static List<Models.Song> GetSeedingSongs() => new List<Models.Song>()
    {
      new() { Id=1, Name="The Dying Song", Artist="Slipknot", ImageUrl=""},
      new() { Id=2, Name="Faint", Artist="Linkin Park", ImageUrl=""},
      new() { Id=3, Name="Shogun", Artist="Trivium", ImageUrl=""},
      new() { Id=4, Name="Like You Do", Artist="Joji", ImageUrl=""},
    };
  }
}