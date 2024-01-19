using MediatR;
using Microsoft.AspNetCore.Http;
using MusicPracticePlanner.Base.ServicePrimitives.Integration;
using RepertoireDomain;
using RepertoireDomain.Entities;
using StorageService;

namespace RepertoireService;

public abstract class AddMusic
{   
    public record Command(Guid StudentId, string Name, IFormFile File):IRequest<Guid> {}

    public class Handler : IRequestHandler<Command, Guid>
    {
        private IBus _bus;
        private IStorageService _storageService;
        private IRepertoireRepository _repertoireRepository;

        public Handler(IStorageService storageService, IRepertoireRepository repertoireRepository, IBus bus)
        {
            _storageService = storageService;
            _repertoireRepository = repertoireRepository;
            _bus = bus;
        }

        public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
        {
            Repertoire repertorie = await _repertoireRepository.FindRepertoireByStudent(request.StudentId, cancellationToken);
            
            if(repertorie == null) {
                repertorie = Repertoire.Create(request.StudentId);
            }

            var fileUrl = await _storageService.Save(request.File, cancellationToken);
            Music music = repertorie.AddMusic(request.Name, fileUrl.Url);

            await _repertoireRepository.Save(repertorie, cancellationToken);
            await _bus.Send(new MidiFileAdded(music.Id, fileUrl.Url));

            return music.Id;
        }
    }

}
