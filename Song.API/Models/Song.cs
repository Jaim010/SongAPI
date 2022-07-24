using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Song.API.Models
{
  public class Song
  {
#pragma warning disable CS8618
    [DefaultValue(0)]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Artist { get; set; }

    [Required]
    public string ImageUrl { get; set; }
#pragma warning restore CS8618
  }
}