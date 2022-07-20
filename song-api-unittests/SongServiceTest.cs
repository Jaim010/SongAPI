using Song_Api.Models;
using Song_Api.Services;

namespace Song_Api_Unittests;

public class SongServiceTest : SongDataSeed
{
  [Fact]
  public async void GetSongsTest()
  {
    // Arrange
    var service = new SongService(this.context);

    // Act
    (var songsIEnum, var result) = (await service.GetSongs());

    // Assert
    Assert.Equal(Result.Ok, result);

    Assert.NotNull(songsIEnum);
    var songs = songsIEnum?.ToList();
    Assert.Equal(4, songs?.Count);

    Assert.Equal("The Dying Song", songs?[0].Name);
    Assert.Equal("Faint", songs?[1].Name);
    Assert.Equal("Shogun", songs?[2].Name);
    Assert.Equal("Like You Do", songs?[3].Name);
  }

  [Fact]
  public async void GetSongTheory_GivenValidID_ReturnsSong()
  {
    // Arrange
    var id = 1;
    var service = new SongService(this.context);

    // Act
    (var song, var result) = await service.GetSong(id);

    // Assert
    Assert.Equal(Result.Ok, result);
    Assert.NotNull(song);
    Assert.Equal("The Dying Song", song?.Name);
  }

  [Theory]
  [InlineData(5, Result.NotFound, null)]
  [InlineData(-1, Result.NotFound, null)]
  public async void InvalidGetSongTheory(int id, Result expectedResult, Song expectedSong)
  {
    // Arrange
    var service = new SongService(this.context);

    // Act
    (var song, var result) = await service.GetSong(id);

    // Assert
    Assert.Equal(expectedResult, result);
    Assert.Equal(expectedSong, song);
  }

  [Fact]
  public async void PostAlbum_GivenValidSongAndID_ReturnsSong()
  {
    // Arrange
    var service = new SongService(this.context);
    var newSong = new Song()
    {
      Id = 5,
      Name = "Potato Salad",
      Artist = "Tyler The Creator",
      ImageUrl = ""
    };

    // Act
    (var song, var result) = await service.AddSong(newSong);

    // Assert
    Assert.Equal(Result.Ok, result);
    Assert.NotNull(song);
    Assert.Equal(newSong.Name, song?.Name);
  }

  [Fact]
  public async void PutAlbum_GivenValidSongAndID_ReturnsResultOk()
  {
    // Arrange
    var service = new SongService(this.context);
    var id = 1;

    (var song, var result) = await service.GetSong(id);

    if (song == null)
      throw new Exception($"service.GetSong() with id: {id} returned 'null'");

    song.Artist = "Eminem";
    song.Name = "25 To Life";

    // Act
    result = await service.UpdateSong(id, song);

    // Assert
    Assert.Equal(Result.Ok, result);
  }

  [Fact]
  public async void PutAlbum_GivenValidSongAndNotMatchingID_ReturnsResultBadRequest()
  {
    // Arrange
    var service = new SongService(this.context);
    var id = 1;
    var notMatchingId = -1;

    // Issue due to EF tracking. Underlying EF ID is kept, unable to test otherwise. 
    (var song, var result) = await service.GetSong(id);

    if (song == null)
      throw new Exception($"service.GetSong() with id: {id} returned 'null'");

    song.Artist = "Eminem";
    song.Name = "25 To Life";

    // Act
    result = await service.UpdateSong(notMatchingId, song);

    // Assert
    Assert.Equal(Result.BadRequest, result);
  }

  [Fact]
  public async void DeleteAlbum_GivenValidID_ReturnsResultOK()
  {
    // Arrange
    var service = new SongService(this.context);
    var id = 1;

    // Act
    var result = await service.DeleteSong(id);

    // Assert
    Assert.Equal(Result.Ok, result);
  }

  [Fact]
  public async void DeleteAlbum_GivenInValidID_ReturnsResultNotFound()
  {
    // Arrange
    var service = new SongService(this.context);
    var id = -1;

    // Act
    var result = await service.DeleteSong(id);

    // Assert
    Assert.Equal(Result.NotFound, result);
  }
}