using Microsoft.EntityFrameworkCore;

namespace Song_Api.PostgreSQL
{
  public class SongContext : DbContext
  {
    public DbSet<Models.Song> Songs { get; set; }
    public SongContext(DbContextOptions<SongContext> options) : base(options) {}
  }
}