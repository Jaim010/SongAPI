using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Song_Api.PostgreSQL;

namespace Song_Api_Intergrationtests
{
  public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
  {
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
      builder.ConfigureServices(services =>
      {
        var descriptor = services.SingleOrDefault(
          d => d.ServiceType ==
            typeof(DbContextOptions<SongContext>));

        services.Remove(descriptor);

        services.AddDbContext<SongContext>(options =>
        {
          options.UseInMemoryDatabase("InMemoryDbForTesting");
        });

        var sp = services.BuildServiceProvider();

        using (var scope = sp.CreateScope())
        {
          var scopedServices = scope.ServiceProvider;
          var db = scopedServices.GetRequiredService<SongContext>();
          var logger = scopedServices
            .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

          db.Database.EnsureCreated();

          try
          {
            Utilities.InitializeDbForTets(db);
          }
          catch (Exception ex)
          {
            logger.LogError(ex, "An error occurred seeding the " +
                        "database with test messages. Error: {Message}", ex.Message);
          }
        }

      });
    }
  }
}