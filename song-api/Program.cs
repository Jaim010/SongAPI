using Microsoft.EntityFrameworkCore;
using Song_Api.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SongContext>(options
    => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// builder.Services.AddScoped<ISongService, SongService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGet("/health", async context => await context.Response.WriteAsync("healthy"));

DbInitializer.CreateDbIfNotExists(app);

app.Run();
