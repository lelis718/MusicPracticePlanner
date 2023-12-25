using MusicService.Infrastructure;
using MusicPracticePlanner.FileService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddFileServiceInfrastructure();
builder.AddMusicServiceInfrastructure();

var app = builder.Build();
app.UseInfrastructure();

app.Run();
