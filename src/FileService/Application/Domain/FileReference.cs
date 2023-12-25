using AutoMapper;
using L718Framework.Core.Helpers.Extensions;
using MusicPracticePlanner.FileService.Application.Dtos;

namespace MusicPracticePlanner.FileService.Application.Domain;

public class FileReference
{
    public string Filename { get; private set; }

    public string ContentType { get; private set; }
    public string Scope { get; private set; }

    public Stream FileStream { get; private set; }


    public FileReference(string filename, string contentType, string scope, Stream filestream)
    {
        this.Filename = GenerateUniqueFileName(filename, scope);
        this.FileStream = filestream;
        this.ContentType = contentType;
    }

    private string GenerateUniqueFileName(string filename, string scope)
    {
        var filenameWithoutExtension = Path.GetFileNameWithoutExtension(filename);
        filenameWithoutExtension = filenameWithoutExtension.GenerateSlug();
        var extension = Path.GetExtension(filename);
        var internalscope = scope.GenerateSlug();
        return $"{internalscope}/{Guid.NewGuid()}_{filenameWithoutExtension}{extension}";
    }
    private static string ExtractScopeFromFilename(string filePath)
    {
        return Path.GetDirectoryName(filePath);
    }
    private static string ExtractContentTypeFromFilename(string filename)
    {

        string DefaultContentType = "application/octet-stream";


        return DefaultContentType;
    }


    public static FileReference Create(UploadFileDto uploadFileDto)
    {
        return new FileReference(uploadFileDto.FileName, uploadFileDto.ContentType, uploadFileDto.Scope, uploadFileDto.FileContents);
    }
    public static FileReference Create(string fileName, Stream fileStream)
    {
        var scope = ExtractScopeFromFilename(fileName);
        var contentType = ExtractContentTypeFromFilename(fileName);
        return new FileReference(fileName, contentType, scope, fileStream);
    }

}