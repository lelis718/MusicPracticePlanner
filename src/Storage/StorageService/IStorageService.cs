using Microsoft.AspNetCore.Http;

namespace StorageService;

public interface IStorageService
{
    Task Remove(string fileUrl, CancellationToken cancellationToken);
    Task<FileReference> Save(IFormFile file, CancellationToken cancellationToken);
}
