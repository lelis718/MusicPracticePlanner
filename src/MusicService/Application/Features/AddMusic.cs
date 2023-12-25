using AutoMapper;
using FluentValidation;
using MediatR;
using MusicPracticePlanner.MusicService.Application.Domain;
using MusicPracticePlanner.MusicService.Application.Domain.Model;
using MusicPracticePlanner.MusicService.Application.Dtos;

namespace MusicPracticePlanner.MusicService.Application.Features;


public static class AddMusic {

    public sealed record Command : IRequest<MusicDto>{
        public readonly AddMusicDto addMusicDto;
        public Command(AddMusicDto addMusicDto){
            this.addMusicDto = addMusicDto;
        }
    }

   public sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.addMusicDto).NotNull();
            RuleFor(x => x.addMusicDto.Name).NotEmpty();
            RuleFor(x => x.addMusicDto.PdfFile).NotEmpty();
        }
    }
    public sealed class Handler : IRequestHandler<Command, MusicDto>
    {
        private IMusicRpository _repository;
        private IMapper _mapper;

        public Handler(IMusicRpository musicRpository, IMapper mapper){
            _repository = musicRpository;
            _mapper = mapper;
        }

        public async Task<MusicDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var music = Music.Create(request.addMusicDto.Name, request.addMusicDto.PdfFile);
            await _repository.AddAsync(music);
            return _mapper.Map<MusicDto>(music);
        }
    }

}