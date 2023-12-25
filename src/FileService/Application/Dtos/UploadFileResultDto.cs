using System.IO;
namespace MusicPracticePlanner.FileService.Application.Dtos;
public sealed record UploadFileResultDto
{
    public string FileAddress { get; private set; }
    public UploadFileResultDto(string fileAddress)
    {
        this.FileAddress = fileAddress;
    }
}