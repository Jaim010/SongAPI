using Microsoft.EntityFrameworkCore;

namespace Song_Api.PostgreSQL
{
  public class SongContext : DbContext
  {
    public DbSet<Models.Song> Songs { get; set; }
    
#pragma warning disable CS8618
    public SongContext(DbContextOptions<SongContext> options) : base(options) { }
#pragma warning restore CS8618
  }
}