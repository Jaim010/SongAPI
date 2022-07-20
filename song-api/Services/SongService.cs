using Microsoft.EntityFrameworkCore;
using Song_Api.Models;
using Song_Api.PostgreSQL;

namespace Song_Api.Services
{
  public class SongService : ISongService
  {
    private readonly SongContext _context;

    public SongService(SongContext context)
      => _context = context;

    public async Task<Tuple<IEnumerable<Song>?, Result>> GetSongs()
    {
      if (_context.Songs == null)
      {
        return new(null, Result.NotFound);
      }
      return new(await _context.Songs.ToListAsync(), Result.Ok);
    }

    public async Task<Tuple<Song?, Result>> GetSong(int id)
    {
      if (_context.Songs == null)
        return new(null, Result.NotFound);

      Song? song = await _context.Songs.FindAsync(id);

      if (song == null)
        return new(null, Result.NotFound);

      return new(song, Result.Ok);
    }

    public async Task<Result> PutSong(int id, Song song)
    {
      if (id != song.Id)
        return Result.BadRequest;

      _context.Entry(song).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!SongExists(id))
        {
          return Result.NotFound;
        }
        else
        {
          throw;
        }
      }

      return Result.Ok;
    }

    public async Task<Tuple<Song?, Result>> PostSong(Song song)
    {
      if (_context.Songs == null)
        return new(null, Result.Err);

      _context.Songs.Add(song);
      await _context.SaveChangesAsync();

      return new(song, Result.Ok);
    }

    public async Task<Result> DeleteSong(int id)
    {
      if (_context.Songs == null)
        return Result.NotFound;

      var song = await _context.Songs.FindAsync(id);
      if (song == null)
        return Result.NotFound;

      _context.Songs.Remove(song);
      await _context.SaveChangesAsync();

      return Result.Ok;
    }

    private bool SongExists(int id)
    {
      return (_context.Songs?.Any(e => e.Id == id)).GetValueOrDefault();
    }

  }
}
