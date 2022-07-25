using Microsoft.EntityFrameworkCore;
using Song.API.Models;
using Song.API.PostgreSQL;

namespace Song.API.Services
{
  public class SongService : ISongService
  {
    private readonly SongContext _context;

    public SongService(SongContext context)
      => _context = context;


    public async Task<ServiceResponse<IEnumerable<Models.Song>>> GetSongs()
    {
      if (_context.Songs == null)
        return new()
        {
          Result = Result.NotFound,
        };

      return new()
      {
        Data = await _context.Songs.ToListAsync(),
        Result = Result.Ok,
      };
    }

    public async Task<ServiceResponse<Models.Song>> GetSong(int id)
    {
      if (_context.Songs == null)
        return new()
        {
          Result = Result.NotFound,
        };

      Models.Song? song = await _context.Songs.FindAsync(id);

      if (song == null)
        return new() { Result = Result.NotFound };

      return new()
      {
        Data = song,
        Result = Result.Ok,
      };
    }

    public async Task<ServiceResponse> UpdateSong(int id, Models.Song song)
    {
      if (id != song.Id)
        return new() { Result = Result.BadRequest };

      _context.Entry(song).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateException)
      {
        if (!SongExists(id))
          return new() { Result = Result.NotFound };
        else
          throw;
      }

      return new() { Result = Result.Ok };
    }

    public async Task<ServiceResponse<Models.Song>> AddSong(Models.Song song)
    {
      if (_context.Songs == null)
        return new() { Result = Result.Err };

      _context.Songs.Add(song);
      await _context.SaveChangesAsync();

      return new()
      {
        Result = Result.Ok,
        Data = song,
      };
    }

    public async Task<ServiceResponse> DeleteSong(int id)
    {
      if (_context.Songs == null)
        return new() { Result = Result.NotFound };

      Models.Song? song = await _context.Songs.FindAsync(id);

      if (song == null)
        return new() { Result = Result.NotFound };

      _context.Songs.Remove(song);
      await _context.SaveChangesAsync();

      return new() { Result = Result.Ok };
    }

    private bool SongExists(int id)
    {
      return (_context.Songs?.Any(e => e.Id == id)).GetValueOrDefault();
    }

  }
}
