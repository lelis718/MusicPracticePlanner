namespace MusicPracticePlanner.Base.ServicePrimitives;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
