using Microsoft.AspNetCore.Mvc.Testing;

namespace SongAPI.IntergrationTests {
  public class HealthEndPointTest {
    private readonly HttpClient _client;

    public HealthEndPointTest() {
      var appFactory = new WebApplicationFactory<Program>();
      _client = appFactory.CreateClient();
    }

    [Fact]
    public async void GetHealth() {
      // Arrange
      
      // Act
      var response = await _client.GetAsync("/health/");
      var responseStr = await response.Content.ReadAsStringAsync();

      // Assert
      response.EnsureSuccessStatusCode();
      Assert.NotNull(responseStr);      
      Assert.Equal("healthy", responseStr);
    }
  }
}