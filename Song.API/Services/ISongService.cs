using Song.API.Models;

namespace Song.API.Services
{
  public interface ISongService
  {
    public Task<ServiceResponse<IEnumerable<Models.Song>>> GetSongs();
    public Task<ServiceResponse<Models.Song>> GetSong(int id);
    public Task<ServiceResponse> UpdateSong(int id, Models.Song song);
    public Task<ServiceResponse<Models.Song>> AddSong(Models.Song song);
    public Task<ServiceResponse> DeleteSong(int id);
  }
}