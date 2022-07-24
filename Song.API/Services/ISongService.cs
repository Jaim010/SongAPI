using Song.API.Models;

namespace Song.API.Services
{
  public interface ISongService
  {
    public Task<Tuple<IEnumerable<Models.Song>?, Result>> GetSongs();
    public Task<Tuple<Models.Song?, Result>> GetSong(int id);
    public Task<Result> UpdateSong(int id, Models.Song song);
    public Task<Tuple<Models.Song?, Result>> AddSong(Models.Song song);
    public Task<Result> DeleteSong(int id);
  }
}