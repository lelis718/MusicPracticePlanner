using MediatR;
using MusicPracticePlanner.MusicService.Application.Domain;
using MusicPracticePlanner.MusicService.Application.Dtos;

namespace MusicPracticePlanner.MusicService.Application.Features;

public static class GetMusics{
    public sealed record Query : IRequest<IList<MusicDto>> {

    }

    public sealed class Handler : IRequestHandler<Query, IList<MusicDto>>
    {
        private IMusicRpository _repository;

        public Handler(IMusicRpository musicRpository){
            _repository = musicRpository;
        }

        public async Task<IList<MusicDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _repository.FindMusicsAsync();
                        
        }
    }
}