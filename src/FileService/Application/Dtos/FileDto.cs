using System.IO;
using AutoMapper;
using MusicPracticePlanner.FileService.Application.Domain;

namespace MusicPracticePlanner.FileService.Application.Dtos;

[AutoMap(typeof(FileReference))]

public sealed record FileDto
{
    public string Filename { get; private set; }
    public string ContentType { get; private set; }
    public string Scope { get; private set; }
    public Stream FileStream { get; private set; }

    public FileDto(string filename, string contentType, string scope, Stream fileStream)
    {
        this.Filename = filename;
        this.ContentType = contentType;
        this.Scope = scope;
        this.FileStream = fileStream;
    }
}