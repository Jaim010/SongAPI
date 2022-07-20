using SongAPI.Models;

namespace SongAPI.Services
{
  public interface ISongService
  {
    public Task<Tuple<IEnumerable<Song>?, Result>> GetSongs();
    public Task<Tuple<Song?, Result>> GetSong(int id);
    public Task<Result> UpdateSong(int id, Song song);
    public Task<Tuple<Song?, Result>> AddSong(Song song);
    public Task<Result> DeleteSong(int id);
  }
}