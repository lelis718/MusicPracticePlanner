using Microsoft.AspNetCore.Http;

namespace StorageService;

public interface IStorageService
{
    Task<FileReference> Save(IFormFile file, CancellationToken cancellationToken);
}
