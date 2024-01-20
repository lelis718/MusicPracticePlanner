namespace MusicPracticePlanner.Base.ServicePrimitives.Integration;

public interface IBus
{
    Task Send(IIntegrationEvent @event, CancellationToken cancellationToken);
}
