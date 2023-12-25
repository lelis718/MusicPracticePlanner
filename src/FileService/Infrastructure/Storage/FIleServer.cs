using L718Framework.Core.Helpers.Extensions;
using MusicPracticePlanner.FileService.Application.Domain;
using MusicPracticePlanner.FileService.Application.Dtos;

namespace MusicPracticePlanner.FileService.Infrastructure.Storage;

public class FileStorage : IFileStorage
{

    private string storageVault;
    public FileStorage(string vault){
        this.storageVault = vault;
    }

    public async Task<Stream> LoadFileAsync(string file)
    {
        var filePath = GetFilePath(file);
        if(File.Exists(filePath)){
            var bytes = await File.ReadAllBytesAsync(filePath);
            return new MemoryStream(bytes);
        }
        return null;
    }

    public async Task<string> UploadFileAsync(FileReference fileReference)
    {
        var filePath = GetFilePath(fileReference.Filename);

        EnsureDirectoryExists(filePath);
        DeleteFileIfExists(filePath);

        using (var fileStream = File.Create(filePath))
        {
            fileReference.FileStream.Seek(0, SeekOrigin.Begin);
            await fileReference.FileStream.CopyToAsync(fileStream);
        }
        return fileReference.Filename;
    }

    private static void EnsureDirectoryExists(string filePath)
    {
        var directoryPath = Path.GetDirectoryName(filePath);
        Directory.CreateDirectory(directoryPath);
    }

    private static void DeleteFileIfExists(string filePath)
    {
        //Just in case...
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    private string GetFilePath(string fileName){
        return Path.Combine(this.storageVault, fileName);
    }
}
