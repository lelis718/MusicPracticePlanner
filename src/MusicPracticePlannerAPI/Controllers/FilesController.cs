using L718Framework.Infrastructure.Controllers;
using Microsoft.AspNetCore.Mvc;
using MusicPracticePlanner.FileService.Application.Dtos;
using MusicPracticePlanner.FileService.Application.Features;

namespace MusicPracticePlannerAPI.Controllers;

public class FilesController : BaseApiController
{
    [HttpPost(Name =  nameof(UploadFileAsync))]
    public async Task<ActionResult> UploadFileAsync(IFormFile formFile, string scope)
    {
        var command = new UploadFile.Command(new UploadFileDto(formFile?.FileName,formFile?.ContentType, scope, formFile?.OpenReadStream()));
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpGet(Name=nameof(LoadFile))]
    public async Task<ActionResult> LoadFile(string filename){
        var query = new LoadFile.Query(filename);
        var result = await Mediator.Send(query);

        return new FileStreamResult(result.FileStream, result.ContentType);

    }

}
