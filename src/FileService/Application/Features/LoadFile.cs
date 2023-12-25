

using System.Data;
using System.Runtime.CompilerServices;
using AutoMapper;
using FluentValidation;
using MediatR;
using MusicPracticePlanner.FileService.Application.Domain;
using MusicPracticePlanner.FileService.Application.Dtos;

namespace MusicPracticePlanner.FileService.Application.Features;

public static class LoadFile
{
    public sealed class Query : IRequest<FileDto>
    {

        public string Filename { get; private set; }
        public Query(string filename)
        {
            this.Filename = filename;
        }
    }

    public sealed class Handler : IRequestHandler<Query, FileDto>
    {
        private IFileStorage _fileStorage;
        private IMapper _mapper;
        public Handler(IFileStorage fileStorage, IMapper mapper)
        {
            _fileStorage = fileStorage;
            _mapper = mapper;
        }

        public async Task<FileDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var fileStream = await _fileStorage.LoadFileAsync(request.Filename);
            if (fileStream == null) return null;

            var fileReference = FileReference.Create(request.Filename, fileStream);

            return _mapper.Map<FileDto>(fileReference);

        }
    }
}