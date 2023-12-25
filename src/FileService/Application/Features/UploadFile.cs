

using System.Data;
using System.Runtime.CompilerServices;
using FluentValidation;
using MediatR;
using MusicPracticePlanner.FileService.Application.Domain;
using MusicPracticePlanner.FileService.Application.Dtos;

namespace MusicPracticePlanner.FileService.Application.Features;

public static class UploadFile
{
    public sealed class Command : IRequest<UploadFileResultDto>
    {

        public UploadFileDto FileData { get; private set; }
        public Command(UploadFileDto fileData)
        {
            this.FileData = fileData;
        }
    }

    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.FileData).NotNull();
            RuleFor(x => x.FileData.FileName).NotEmpty();
            RuleFor(x => x.FileData.FileContents).NotEmpty();
        }
    }

    public sealed class Handler : IRequestHandler<Command, UploadFileResultDto>
    {
        private IFileStorage _fileStorage;
        public Handler(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        public async Task<UploadFileResultDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var fileReference = FileReference.Create(request.FileData);
            var fileString = await _fileStorage.UploadFileAsync(fileReference);
            return new UploadFileResultDto(fileString);
        }
    }
}