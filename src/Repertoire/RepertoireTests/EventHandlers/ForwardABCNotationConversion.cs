using MediatR;
using MusicPracticePlanner.Base.ServicePrimitives.Integration;
using RepertoireDomain.Events;

namespace RepertoireService;

internal class ForwardABCNotationConversion : INotificationHandler<ABCNotationConversionNeeded>
{
    private IBus _bus;

    public ForwardABCNotationConversion(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(ABCNotationConversionNeeded notification, CancellationToken cancellationToken)
    {
        await _bus.Send(new MidiFileAdded(notification.MusicId, notification.MidiFileUrl), cancellationToken);
    }
}