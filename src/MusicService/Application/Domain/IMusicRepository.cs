using L718Framework.Core.Domain;
using MusicPracticePlanner.MusicService.Application.Domain.Model;
using MusicPracticePlanner.MusicService.Application.Dtos;

namespace MusicPracticePlanner.MusicService.Application.Domain;


public interface IMusicRpository : IRepository<Music>
{
    Task<IList<MusicDto>> FindMusicsAsync();
}