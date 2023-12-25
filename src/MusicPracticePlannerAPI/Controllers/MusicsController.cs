using L718Framework.Infrastructure.Controllers;
using Microsoft.AspNetCore.Mvc;
using MusicPracticePlanner.MusicService.Application.Dtos;
using MusicPracticePlanner.MusicService.Application.Features;

namespace MusicPracticePlannerAPI.Controllers;

public class MusicsController : BaseApiController {
    
    [HttpPost(Name = nameof(AddMusicAsync))]
    [ProducesResponseType(200, Type=typeof(MusicDto))]
    public async Task<ActionResult> AddMusicAsync(AddMusicDto request){

        var command = new AddMusic.Command(request);
        var commandResponse = await Mediator.Send(command);
        return Ok(commandResponse);
    } 
    
    [HttpGet(Name =nameof(GetMusicsAsync))]
    [ProducesResponseType(200, Type=typeof(IList<MusicDto>))]
    public async Task<ActionResult> GetMusicsAsync(){
        var query = new GetMusics.Query();
        var queryResult = await Mediator.Send(query);
        return Ok(queryResult);
    } 
}