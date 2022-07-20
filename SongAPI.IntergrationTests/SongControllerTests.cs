using System.Net;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using SongAPI.Models;

namespace SongAPI.IntergrationTests
{
  public class SongControllerTests
      : IClassFixture<CustomWebApplicationFactory<Program>>
  {
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public SongControllerTests(CustomWebApplicationFactory<Program> factory)
    {
      _factory = factory;
      _client = factory.CreateClient(new WebApplicationFactoryClientOptions
      {
        AllowAutoRedirect = false,
      });
    }

    [Fact]
    public async void GetSongs()
    {
      // Arrange 

      // Act
      var response = await _client.GetAsync("/api/song/");
      var responseString = await response.Content.ReadAsStringAsync();
      var songs = JsonSerializer.Deserialize<List<Song>>(responseString, _jsonSerializerOptions);

      // Assert
      response.EnsureSuccessStatusCode();
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      Assert.NotNull(songs);
      Assert.Equal(4, songs?.Count);
    }

    [Fact]
    public async void GetSong_GivenID_ReturnsSong()
    {
      // Arrange 
      var id = 1;

      // Act
      var response = await _client.GetAsync($"/api/song/{id}");
      var responseString = await response.Content.ReadAsStringAsync();
      var song = JsonSerializer.Deserialize<Song>(responseString, _jsonSerializerOptions);

      // Assert
      response.EnsureSuccessStatusCode();
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      Assert.NotNull(song);
      Assert.Equal(id, song?.Id);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(213)]
    public async void TheoryGetSong_GivenNonExistingID_ReturnsNotFound(int id)
    {
      // Arrange 

      // Act
      var response = await _client.GetAsync($"/api/song/{id}");

      // Assert
      Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }


    // Arrange 
    // Act
    // Assert
  }
}
