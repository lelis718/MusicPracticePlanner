using System.Data;
using Dapper;
using L718Framework.Persistence.DapperDatabase;
using MusicPracticePlanner.MusicService.Application.Domain;
using MusicPracticePlanner.MusicService.Application.Domain.Model;
using MusicPracticePlanner.MusicService.Application.Dtos;

namespace MusicPracticePlanner.MusicService.Infrastructure.Database;

public class MusicRepository : RepositoryBase<Music,Guid>, IMusicRpository
{
    public MusicRepository(IDbConnection connection) : base(connection)
    {}

    public async Task<IList<MusicDto>> FindMusicsAsync()
    {
        var query = "Select * from Musics";
        var result = await Connection.QueryAsync<MusicDto>(query);
        return result.ToList();
    }
}