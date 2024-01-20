using MediatR;
using MusicPracticePlanner.Base.ServicePrimitives.Integration;
using RepertoireDomain.Events;
using StorageService;

namespace RepertoireService;

internal class RemoveMidiFile : INotificationHandler<MidiFileRemoved>
{
    private IStorageService _storageService;

    public RemoveMidiFile(IStorageService storageService)
    {
        _storageService = storageService;
    }

    public async Task Handle(MidiFileRemoved notification, CancellationToken cancellationToken)
    {
        await _storageService.Remove(notification.MidiFileUrl, cancellationToken);
    }
}