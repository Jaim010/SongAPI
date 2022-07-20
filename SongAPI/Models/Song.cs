namespace SongAPI.Models
{
  public class Song
  {
#pragma warning disable CS8618
    public int Id { get; set; }
    public string Name { get; set; }
    public string Artist { get; set; }
    public string ImageUrl { get; set; }
#pragma warning restore CS8618
  }
}