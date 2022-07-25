using Moq;
using Song.API.Controllers;
using Song.API.Models;
using Song.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Song.API.UnitTests
{
  public class SongControllerTest
  {
    [Fact]
    public async void GetSongs_SongsExist_ReturnsOk()
    {
      // Arrange
      var songs = new List<Models.Song> {
          new() { Id=1, Name="The Dying Song", Artist="Slipknot", ImageUrl=""},
          new() { Id=2, Name="Faint", Artist="Linkin Park", ImageUrl=""},
          new() { Id=3, Name="Shogun", Artist="Trivium", ImageUrl=""},
          new() { Id=4, Name="Like You Do", Artist="Joji", ImageUrl=""},
        };

      var songsIEnum = (IEnumerable<Models.Song>)songs;
      var methodResult = new ServiceResponse<IEnumerable<Models.Song>>()
      {
        Data = songsIEnum,
        Result = Result.Ok,
      };

      var songServiceMock = new Mock<ISongService>();

#pragma warning disable CS8620 
      songServiceMock.Setup(s => s.GetSongs()).Returns(Task.FromResult(methodResult));
#pragma warning restore CS8620 

      var controller = new SongController(songServiceMock.Object);

      // Act
      var response = await controller.GetSongs();

      // Assert
      var okObjectResult = Assert.IsType<OkObjectResult>(response);
      var returnValue = Assert.IsType<List<Models.Song>>(okObjectResult.Value);
      Assert.Equal(songs.Count, returnValue.Count);
      Assert.Equal(songs[0].Name, returnValue[0].Name);
      Assert.Equal(songs[1].Name, returnValue[1].Name);
      Assert.Equal(songs[2].Name, returnValue[2].Name);
      Assert.Equal(songs[3].Name, returnValue[3].Name);
    }

    [Fact]
    public async void GetSongs_NoSongs_ReturnsNotFound()
    {
      // Arrange
      var songs = new List<Models.Song>();

      var songsIEnum = (IEnumerable<Models.Song>)songs;
      var methodResult = new ServiceResponse<IEnumerable<Models.Song>>()
      {
        Data = songsIEnum,
        Result = Result.NotFound,
      };

      var songServiceMock = new Mock<ISongService>();

#pragma warning disable CS8620 
      songServiceMock.Setup(s => s.GetSongs()).Returns(Task.FromResult(methodResult));
#pragma warning restore CS8620 

      var controller = new SongController(songServiceMock.Object);

      // Act
      var response = await controller.GetSongs();

      // Assert
      Assert.IsType<NotFoundResult>(response);
    }

    [Fact]
    public async void GetSong_GivenValidID_ReturnsOk()
    {
      // Arrange
      var id = 1;
      var song = new Models.Song() { Id = 1, Name = "The Dying Song", Artist = "Slipknot", ImageUrl = "" };

      var songServiceMock = new Mock<ISongService>();
      var methodResult = new ServiceResponse<Models.Song>()
      {
        Data = song,
        Result = Result.Ok,
      };

#pragma warning disable CS8620 
      songServiceMock.Setup(s => s.GetSong(It.IsAny<int>())).Returns(Task.FromResult(methodResult));
#pragma warning restore CS8620

      var controller = new SongController(songServiceMock.Object);

      // Act
      var response = await controller.GetSong(id);

      // Assert      
      var okObjectResult = Assert.IsType<OkObjectResult>(response);
      var returnValue = Assert.IsType<Models.Song>(okObjectResult.Value);
      Assert.NotNull(returnValue);
      Assert.Equal(song.Name, returnValue.Name);
      Assert.Equal(id, returnValue.Id);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(5)]
    public async void InvalidGetSongTheory_ReturnsNotFound(int id)
    {
      // Arrange
      var song = new Models.Song() { Id = 1, Name = "The Dying Song", Artist = "Slipknot", ImageUrl = "" };

      var songServiceMock = new Mock<ISongService>();
      var methodResult = new ServiceResponse<Models.Song>()
      {
        Data = song,
        Result = Result.NotFound
      };

#pragma warning disable CS8620 
      songServiceMock.Setup(s => s.GetSong(It.IsAny<int>())).Returns(Task.FromResult(methodResult));
#pragma warning restore CS8620

      var controller = new SongController(songServiceMock.Object);

      // Act
      var response = await controller.GetSong(id);

      // Assert      
      Assert.IsType<NotFoundResult>(response);
    }

    [Fact]
    public async void PostSong_GivenValidSong_ReturnsCreatedAt()
    {
      // Arrange
      var newSong = new Models.Song() { Id = 1, Name = "The Dying Song", Artist = "Slipknot", ImageUrl = "" };
      var methodResult = new ServiceResponse<Models.Song>()
      {
        Data = newSong,
        Result = Result.Ok
      };

      var songServiceMock = new Mock<ISongService>();

#pragma warning disable CS8620
      songServiceMock.Setup(s => s.AddSong(It.IsAny<Models.Song>())).Returns(Task.FromResult(methodResult));
#pragma warning restore CS8620

      var controller = new SongController(songServiceMock.Object);

      // Act
      var response = await controller.PostSong(newSong);

      // Assert 
      var createdAtResult = Assert.IsType<CreatedAtActionResult>(response);
      var returnValue = Assert.IsType<Models.Song>(createdAtResult.Value);
      Assert.Equal(newSong, returnValue);
    }

    [Fact]
    public async void PutSong_GivenValidSongAndMatchingID_ReturnsNoContent()
    {
      // Arrange
      var songServiceMock = new Mock<ISongService>();
      var methodResult = new ServiceResponse() { Result = Result.Ok };
      songServiceMock.Setup(s => s.UpdateSong(It.IsAny<int>(), It.IsAny<Models.Song>())).Returns(Task.FromResult(methodResult));

      var controller = new SongController(songServiceMock.Object);

      var id = 1;
      var updatedSong = new Models.Song() { Id = 1, Name = "The Dying Song", Artist = "Slipknot", ImageUrl = "" };

      // Act
      var response = await controller.PutSong(id, updatedSong);

      // Assert
      Assert.IsType<NoContentResult>(response);
    }

    [Fact]
    public async void PutSong_GivenValidSongAndNotMatchingID_ReturnsBadRequest()
    {
      // Arrange
      var songServiceMock = new Mock<ISongService>();
      var methodResult = new ServiceResponse() { Result = Result.BadRequest };
      songServiceMock.Setup(s => s.UpdateSong(It.IsAny<int>(), It.IsAny<Models.Song>())).Returns(Task.FromResult(methodResult));

      var controller = new SongController(songServiceMock.Object);

      var id = 1;
      var updatedSong = new Models.Song() { Id = 1, Name = "The Dying Song", Artist = "Slipknot", ImageUrl = "" };

      // Act
      var response = await controller.PutSong(id, updatedSong);

      // Assert
      Assert.IsType<BadRequestResult>(response);
    }

    [Fact]
    public async void DeleteAlbum_GivenValidID_ReturnsResultNoContent()
    {
      // Arrange
      var songServiceMock = new Mock<ISongService>();
      var methodResult = new ServiceResponse() { Result = Result.Ok };
      songServiceMock.Setup(s => s.DeleteSong(It.IsAny<int>())).Returns(Task.FromResult(methodResult));

      var controller = new SongController(songServiceMock.Object);

      var id = 1;

      // Act
      var response = await controller.DeleteSong(id);

      // Assert
      Assert.IsType<NoContentResult>(response);
    }


    [Fact]
    public async void DeleteAlbum_GivenNonExistingID_ReturnsNotFound()
    {
      // Arrange
      var songServiceMock = new Mock<ISongService>();
      var methodResult = new ServiceResponse() { Result = Result.NotFound };
      songServiceMock.Setup(s => s.DeleteSong(It.IsAny<int>())).Returns(Task.FromResult(methodResult));

      var controller = new SongController(songServiceMock.Object);

      var id = -1;

      // Act
      var response = await controller.DeleteSong(id);

      // Assert
      Assert.IsType<NotFoundResult>(response);
    }
  }
}