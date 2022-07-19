using Song_Api.Models;

namespace Song_Api.Services
{
  public interface ISongService
  {
    public Task<Tuple<IEnumerable<Song>?, Result>> GetSongs();
    public Task<Tuple<Song?, Result>> GetSong(int id);
    public Task<Result> PutSong(int id, Song song);
    public Task<Tuple<Song?, Result>> PostSong(Song song);
    public Task<Result> DeleteSong(int id);
  }
}