using RepertoireDomain.Entities;

namespace RepertoireDomain;

public interface IRepertoireRepository
{
    Task<Repertoire> FindRepertoireByStudent(Guid studentId, CancellationToken cancellationToken);
    Task Save(Repertoire repertorie, CancellationToken cancellationToken);
}
