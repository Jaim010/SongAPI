using Microsoft.AspNetCore.Mvc;
using Song_Api.Models;
using Song_Api.Services;

namespace song_api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SongController : ControllerBase
  {
    private readonly ISongService _service;

    public SongController(ISongService service)
     => _service = service;

    // GET: api/Song
    [HttpGet]
    public async Task<IActionResult> GetSongs()
    {
      (IEnumerable<Song>? songs, Result result) = await _service.GetSongs();

      if (result == Result.NotFound)
        return NotFound();

      return Ok(songs);
    }

    // GET: api/Song/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSong(int id)
    {
      (Song? song, Result result) = await _service.GetSong(id);

      if (result == Result.NotFound)
        return NotFound();

      return Ok(song);
    }

    // PUT: api/Song/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSong(int id, Song song)
    {
      Result result = await _service.UpdateSong(id, song);
      switch (result)
      {
        case Result.BadRequest:
          return BadRequest();

        case Result.NotFound:
          return NotFound();

        case Result.Ok:
          return NoContent();

        default:
          return StatusCode(500);
      }
    }

    // POST: api/Song
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<IActionResult> PostSong(Song song)
    {
      (Song? postedSong, Result result) = await _service.AddSong(song);

      if (result == Result.Err)
        return Problem("Entity set 'SongContext.Songs'  is null.");

      return CreatedAtAction("GetSong", new { id = song.Id }, song);
    }

    // DELETE: api/Song/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSong(int id)
    {
      Result result = await _service.DeleteSong(id);
      if (result == Result.NotFound)
        return NotFound();

      return NoContent();
    }
  }
}
