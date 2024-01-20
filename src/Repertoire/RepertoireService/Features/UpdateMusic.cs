using MediatR;
using Microsoft.AspNetCore.Http;
using MusicPracticePlanner.Base.ServicePrimitives;
using RepertoireDomain;
using RepertoireDomain.Entities;
using StorageService;

namespace RepertoireService;

public abstract class UpdateMusic
{
    public record Command(Guid StudentId, Guid MusicId, string Name, string? ABCNotation, IFormFile? File) : IRequest<Guid> { }

    public class Handler : IRequestHandler<Command, Guid>
    {
        private IStorageService _storageService;
        private IRepertoireRepository _repertoireRepository;
        private IUnitOfWork _unitOfWork;

        public Handler(IStorageService storageService, IRepertoireRepository repertoireRepository, IUnitOfWork unitOfWork)
        {
            _storageService = storageService;
            _repertoireRepository = repertoireRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
        {
            Repertoire repertorie = await _repertoireRepository.FindRepertoireByStudent(request.StudentId, cancellationToken);

            if (repertorie == null)
            {
                throw new Exception("Repertoire not found");
            }

            if (request.File != null)
            {
                var fileReference = await _storageService.Save(request.File, cancellationToken);
                repertorie.UpdateMusic(request.MusicId, request.Name, request.ABCNotation, fileReference.Url);
            }
            else
            {
                repertorie.UpdateMusic(request.MusicId, request.Name, request.ABCNotation, null);
            }


            await _repertoireRepository.Save(repertorie, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return request.MusicId;
        }
    }

}
