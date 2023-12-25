using System.IO;
namespace MusicPracticePlanner.FileService.Application.Dtos;
public sealed record UploadFileDto
{
    public string FileName { get; private set; }

    public string ContentType { get; private set; }
    public string Scope { get; private set; }
    public Stream FileContents { get; private set; }

    public UploadFileDto(string fileName,string contentType, string scope, Stream fileContents)
    {
        this.FileName = fileName;
        this.ContentType = contentType;
        this.Scope = scope;
        this.FileContents = fileContents;
    }
}