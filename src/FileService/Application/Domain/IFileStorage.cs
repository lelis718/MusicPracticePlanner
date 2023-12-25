using MusicPracticePlanner.FileService.Application.Dtos;

namespace MusicPracticePlanner.FileService.Application.Domain;

public interface IFileStorage
{
    public Task<string> UploadFileAsync(FileReference fileReference);
    public Task<Stream> LoadFileAsync(string filename);
}