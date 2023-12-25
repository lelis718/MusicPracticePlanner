using AutoMapper;
using MusicPracticePlanner.MusicService.Application.Domain.Model;

namespace MusicPracticePlanner.MusicService.Application.Dtos;

[AutoMap(typeof(Music))]
public class MusicDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? PdfFile { get; set; }

}